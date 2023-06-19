using System;
using System.Reflection;

namespace CryptanalystApp.Core.RelayCommand.Helpers
{
    public class WeakFunc<TResult>
    {
        private Func<TResult> _staticFunc;

        protected MethodInfo Method
        {
            get;
            set;
        }

        public bool IsStatic => _staticFunc != null;

        public virtual string MethodName
        {
            get
            {
                if (_staticFunc != null)
                {
                    return _staticFunc.GetMethodInfo().Name;
                }

                return Method.Name;
            }
        }

        protected WeakReference FuncReference
        {
            get;
            set;
        }

        protected object LiveReference
        {
            get;
            set;
        }

        protected WeakReference Reference
        {
            get;
            set;
        }

        public virtual bool IsAlive
        {
            get
            {
                if (_staticFunc == null && Reference == null && LiveReference == null)
                {
                    return false;
                }

                if (_staticFunc != null)
                {
                    if (Reference != null)
                    {
                        return Reference.IsAlive;
                    }

                    return true;
                }

                if (LiveReference != null)
                {
                    return true;
                }

                if (Reference != null)
                {
                    return Reference.IsAlive;
                }

                return false;
            }
        }

        public object Target
        {
            get
            {
                if (Reference == null)
                {
                    return null;
                }

                return Reference.Target;
            }
        }

        protected object FuncTarget
        {
            get
            {
                if (LiveReference != null)
                {
                    return LiveReference;
                }

                if (FuncReference == null)
                {
                    return null;
                }

                return FuncReference.Target;
            }
        }

        protected WeakFunc()
        {
        }

        public WeakFunc(Func<TResult> func, bool keepTargetAlive = false)
            : this(func?.Target, func, keepTargetAlive)
        {
        }

        public WeakFunc(object target, Func<TResult> func, bool keepTargetAlive = false)
        {
            if (func.GetMethodInfo().IsStatic)
            {
                _staticFunc = func;
                if (target != null)
                {
                    Reference = new WeakReference(target);
                }
            }
            else
            {
                Method = func.GetMethodInfo();
                FuncReference = new WeakReference(func.Target);
                LiveReference = (keepTargetAlive ? func.Target : null);
                Reference = new WeakReference(target);
            }
        }

        public TResult Execute()
        {
            if (_staticFunc != null)
            {
                return _staticFunc();
            }

            object funcTarget = FuncTarget;
            if (IsAlive && (object)Method != null && (LiveReference != null || FuncReference != null) && funcTarget != null)
            {
                return (TResult)Method.Invoke(funcTarget, null);
            }

            return default(TResult);
        }

        public void MarkForDeletion()
        {
            Reference = null;
            FuncReference = null;
            LiveReference = null;
            Method = null;
            _staticFunc = null;
        }
    }
}
