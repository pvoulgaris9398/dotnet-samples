using System.Reflection;

namespace Copernicus.Core.Modules
{
    public class ModuleManager(IViewManager viewManager)
    {
#pragma warning disable CA1823
        private readonly IViewManager _viewManager = viewManager;
#pragma warning restore CA1823
        private readonly List<ModuleInstance> _instances = [];

        // TODO: Fetch dynamically
        private static IEnumerable<ModuleDefinition> Modules
        {
            get
            {
                yield return new(
                    "Copernicus.Modules.SecurityMaster",
                    "Copernicus.Modules.SecurityMaster.SecurityMasterModule"
                );
                yield return new(
                    "Copernicus.Modules.Pricing",
                    "Copernicus.Modules.Pricing.PricingModule"
                );
                yield return new(
                    "Copernicus.Modules.CorporateActions",
                    "Copernicus.Modules.CorporateActions.CorporateActionsModule"
                );
            }
        }

        public static void Load(IViewManager vm, ModuleDefinition md, out ModuleInstance mi)
        {
            ArgumentNullException.ThrowIfNull(vm, nameof(vm));
            ArgumentNullException.ThrowIfNull(md, nameof(md));

            var alc = new CopernicusAssemblyLoadContext(Assembly.GetExecutingAssembly().Location);
            var assembly = alc.LoadFromAssemblyName(md.Name);
            var wr = new WeakReference(alc, trackResurrection: true);
            mi = new(wr, alc);
            foreach (var t in assembly.GetTypes())
            {
                if (typeof(IModule).IsAssignableFrom(t))
                {
                    IModule m = Activator.CreateInstance(t) as IModule;
                    m.Initialize(vm);
                }
            }
        }

        public void Load(IViewManager viewManager)
        {
            foreach (var module in Modules)
            {
                ModuleInstance mi;
                Load(viewManager, module, out mi);
                _instances.Add(mi);
            }
        }

        /*
         *  This code doesn't work, obviously, I am keeping a reference and keeping the
         *  context referenced such that it won't unload
         */
        public void Unload()
        {
            foreach (var instance in _instances)
            {
                instance.AssemblyLoadContext.Unload();
                for (int i = 0; instance.WeakReference.IsAlive && (i < 10); i++)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }
        //public void Load()
        //{
        //    foreach (var module in Modules)
        //    {
        //        var context = new CopernicusAssemblyLoadContext(Assembly.GetExecutingAssembly().Location);
        //        var assembly = context.LoadFromAssemblyName(module.Name);

        //        foreach (var t in assembly.GetTypes())
        //        {
        //            if (typeof(IModule).IsAssignableFrom(t))
        //            {
        //                IModule m = Activator.CreateInstance(t) as IModule;
        //                m.Initialize(_viewManager);
        //            }
        //        }

        //        context.Unload();

        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();

        //    }
        //}
    }
}
