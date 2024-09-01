using System.Reflection;

namespace Copernicus.Core.Modules
{
    public class ModuleManager(IViewManager viewManager)
    {
        private readonly IViewManager _viewManager = viewManager;

        // TODO: Fetch dynamically
        private static IEnumerable<ModuleDefinition> Modules
        {
            get
            {
                yield return new("Copernicus.Modules.SecurityMaster", "Copernicus.Modules.SecurityMaster.SecurityMasterModule");
                yield return new("Copernicus.Modules.Pricing", "Copernicus.Modules.Pricing.PricingModule");
                yield return new("Copernicus.Modules.CorporateActions", "Copernicus.Modules.CorporateActions.CorporateActionsModule");
            }
        }

        public void Load()
        {
            foreach (var module in Modules)
            {
                var context = new CopernicusAssemblyLoadContext(Assembly.GetExecutingAssembly().Location);
                var assembly = context.LoadFromAssemblyName(module.Name);

                foreach (var t in assembly.GetTypes())
                {
                    if (typeof(IModule).IsAssignableFrom(t))
                    {
                        IModule m = Activator.CreateInstance(t) as IModule;
                        m.Initialize(_viewManager);
                    }
                }

                context.Unload();

            }

        }
    }
}
