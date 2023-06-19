using Caliburn.Micro;
using CryptanalystApp.Core.Frameworck.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace CryptanalystApp.Bootstrappers
{
    public class AppBootstrapper : Bootstrapper<IShell>
    {
        private CompositionContainer mcontainer;

        public AppBootstrapper() : base()
        {
        }
        protected override void Configure()
        {
            try
            {
                WindowManager windowManager = new WindowManager();
                AggregateCatalog aggregateCatalog = AssemblyVerifier("./");
                AssemblySource.Instance.AddRange(aggregateCatalog.Parts.Select(part => ReflectionModelServices.GetPartType(part).Value.Assembly).Where(assembly => !AssemblySource.Instance.Contains(assembly)));
                AggregateCatalog exportedValue = new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)));

                mcontainer = new CompositionContainer(exportedValue, new ExportProvider[0]);
                CompositionBatch batch = new CompositionBatch();

                batch.AddExportedValue<IWindowManager>((IWindowManager)windowManager);
                batch.AddExportedValue<IEventAggregator>((IEventAggregator)new EventAggregator());
                batch.AddExportedValue<CompositionContainer>(this.mcontainer);
                batch.AddExportedValue<AggregateCatalog>(exportedValue);
                mcontainer.Compose(batch);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.ToString(), "DeviceNetworkManager Bootstrapper Exception", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        protected override object GetInstance(Type service, string key)
        {
            string contractName = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            IEnumerable<object> exportedValues = mcontainer.GetExportedValues<object>(contractName);
            return exportedValues.Count() > 0 ? exportedValues.First() : throw new Exception(string.Format("Could not locate any instances of contract {0}.", contractName));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return mcontainer.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            mcontainer.SatisfyImportsOnce(instance);
        }

        private AggregateCatalog AssemblyVerifier(string path)
        {
            AggregateCatalog aggregateCatalog = new AggregateCatalog();
            byte[] publicKey1 = Assembly.GetExecutingAssembly().GetName().GetPublicKey();
            foreach (string file in Directory.GetFiles(path, "*.dll"))
            {
                AssemblyName assemblyName = (AssemblyName)null;
                try
                {
                    assemblyName = AssemblyName.GetAssemblyName(file);
                }
                catch (ArgumentException ex)
                {
                }
                if (assemblyName != null)
                {
                    byte[] publicKey2 = assemblyName.GetPublicKey();
                    if (publicKey2 != null && ((IEnumerable<byte>)publicKey2).SequenceEqual<byte>((IEnumerable<byte>)publicKey1))
                        aggregateCatalog.Catalogs.Add((ComposablePartCatalog)new AssemblyCatalog(file));
                }
            }
            return aggregateCatalog;
        }
    }
}
