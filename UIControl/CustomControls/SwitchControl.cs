using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace UIControl.CustomControls
{
    public class SwitchControl : Control
    {
        public static readonly DependencyProperty CheckedIndex = DependencyProperty.Register("Index", typeof(int), typeof(SwitchControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIndexChanged)));
        public static readonly DependencyProperty ItemsSource = DependencyProperty.Register("Source", typeof(IEnumerable<string>), typeof(SwitchControl));
        public static readonly DependencyProperty GroupName = DependencyProperty.Register("Group", typeof(string), typeof(SwitchControl));
        private static readonly RoutedEvent IndexChangedEvent = EventManager.RegisterRoutedEvent("IndexChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<int>), typeof(SwitchControl));

        public int Index
        {
            get { return (int)GetValue(CheckedIndex); }
            set { SetValue(CheckedIndex, value); }
        }

        public IEnumerable<string> Source
        {
            get { return (IEnumerable<string>)GetValue(ItemsSource); }
            set { SetValue(ItemsSource, value); }
        }

        public string Group
        {
            get { return (string)GetValue(GroupName); }
            set { SetValue(GroupName, value); }
        }

        static SwitchControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchControl), new FrameworkPropertyMetadata(typeof(SwitchControl)));
        }

        public event RoutedPropertyChangedEventHandler<int> IndexChanged
        {
            add { AddHandler(IndexChangedEvent, value); }
            remove { RemoveHandler(IndexChangedEvent, value); }
        }

        private static void OnIndexChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SwitchControl control = obj as SwitchControl;
            if (control == null)
                return;

            int newValue = (int)args.NewValue;
            int oldValue = (int)args.OldValue;

            RoutedPropertyChangedEventArgs<int> e = new RoutedPropertyChangedEventArgs<int>(oldValue, newValue, IndexChangedEvent);
            control.OnIndexChanged(e);
        }

        protected virtual void OnIndexChanged(RoutedPropertyChangedEventArgs<int> e)
        {
            RaiseEvent(e);
        }
    }
}
