﻿using Copernicus.Core.Modules;
namespace Copernicus.Modules.SecurityMaster
{
    public class SecurityMasterModule : ModuleBase
    {
        public override string Name => "Security Master";

        public override void Initialize(IViewManager viewManager)
        {
            viewManager?.Show(Name, $"Hello, from {Name}");
        }
    }
}
