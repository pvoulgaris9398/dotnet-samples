using Copernicus.Core.Modules;

namespace Copernicus.Modules.Pricing
{
    public sealed class PricingModule : ModuleBase
    {
        public override string Name => "Pricing";

        public override void Initialize(IViewManager viewManager)
        {
            ArgumentNullException.ThrowIfNull(viewManager, nameof(viewManager));
            viewManager.AddView(Name, new MainLayout());
        }
    }
}
