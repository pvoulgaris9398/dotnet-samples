﻿using Copernicus.Core.Modules;
namespace Copernicus.Modules.Pricing
{
    public class PricingModule : ModuleBase
    {
        public override string Name => "Pricing";

        public override void Initialize(IViewManager viewManager)
        {
            viewManager.AddView(Name, new MainLayout());
        }
    }
}
