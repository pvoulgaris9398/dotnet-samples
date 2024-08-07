using System.Reflection;

namespace Copernicus.Core.Modules
{
    public class ModuleManager
    {
        private IViewManager _viewManager;
        public ModuleManager(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        // TODO: Fetch dynamically
        private IEnumerable<ModuleDefinition> GetModules()
        {
            yield return new("Copernicus.Modules.SecurityMaster", "Copernicus.Modules.SecurityMaster.SecurityMasterModule");
            yield return new("Copernicus.Modules.Pricing", "Copernicus.Modules.Pricing.PricingModule");
            yield return new("Copernicus.Modules.CorporateActions", "Copernicus.Modules.CorporateActions.CorporateActionsModule");
        }
        public void Load()
        {
            var moduleLocation = Assembly.GetExecutingAssembly().Location;

            var alc = new CopernicusAssemblyLoadContext(moduleLocation);

            foreach (var module in GetModules())
            {
                Assembly a = alc.Load(module.ModuleName);

                // TODO: Generalize.
                Type? type = Type.GetType(module.TypeName);

                if (type is not null)
                {
                    var moduleInstance = Activator.CreateInstance(type) as IModule;
                    if (moduleInstance is not null)
                    {
                        moduleInstance.Initialize(_viewManager);
                    }
                }
            }

        }
    }
}
