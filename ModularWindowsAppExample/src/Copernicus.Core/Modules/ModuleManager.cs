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
            var moduleLocation = Assembly.GetExecutingAssembly().Location;

            foreach (var module in Modules)
            {
                CopernicusAssemblyLoadContext.Load(module.ModuleName);

                // TODO: Generalize.
                Type? type = Type.GetType(module.TypeName);

                if (type is not null)
                {
                    if (Activator.CreateInstance(type) is not IModule moduleInstance)
                    {
                        continue;
                    }
                    moduleInstance.Initialize(_viewManager);
                }
            }

        }
    }
}
