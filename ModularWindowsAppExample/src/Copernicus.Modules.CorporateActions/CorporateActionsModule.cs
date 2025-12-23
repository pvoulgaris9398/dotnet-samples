using Copernicus.Core.Modules;

namespace Copernicus.Modules.CorporateActions
{
    public class CorporateActionsModule : ModuleBase
    {
        public override string Name => "Corporate Actions";

        public override void Initialize(IViewManager viewManager)
        {
            ArgumentNullException.ThrowIfNull(viewManager, nameof(viewManager));
            viewManager.AddView(Name, new MainLayout());
        }
    }
}
