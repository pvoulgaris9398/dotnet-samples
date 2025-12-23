using System.Reflection;
using System.Runtime.CompilerServices;

namespace Copernicus.Core.Modules
{
    public class ModuleManager2(IViewManager viewManager)
    {
        private readonly List<CopernicusAssemblyLoadContext> _instances = [];

        // TODO: Fetch dynamically
        private static IEnumerable<ModuleDefinition> Modules
        {
            get
            {
                yield return new(
                    "Copernicus.Modules.SecurityMaster",
                    "Copernicus.Modules.SecurityMaster.SecurityMasterModule"
                );
                //yield return new("Copernicus.Modules.Pricing", "Copernicus.Modules.Pricing.PricingModule");
                //yield return new("Copernicus.Modules.CorporateActions", "Copernicus.Modules.CorporateActions.CorporateActionsModule");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static CopernicusAssemblyLoadContext Load(IViewManager vm, ModuleDefinition md)
        {
            ArgumentNullException.ThrowIfNull(md, nameof(md));
            var alc = new CopernicusAssemblyLoadContext(Assembly.GetExecutingAssembly().Location);
            var assembly = alc.LoadFromAssemblyName(md.Name);
            foreach (var t in assembly.GetTypes())
            {
                if (typeof(IModule).IsAssignableFrom(t))
                {
                    IModule m = Activator.CreateInstance(t) as IModule;
                    m.Initialize(null);
                }
            }
            return alc;
        }

        public void Load(IViewManager viewManager)
        {
            foreach (var module in Modules)
            {
                var alc = Load(viewManager, module);
                _instances.Add(alc);
            }
        }

        /*
         *  This code doesn't work, obviously, I am keeping a reference and keeping the
         *  context referenced such that it won't unload
         */
        public void Unload()
        {
            foreach (var alc in _instances)
            {
                var wr = new WeakReference(alc);
                alc.Unload();
                for (int i = 0; wr.IsAlive && (i < 10); i++)
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
