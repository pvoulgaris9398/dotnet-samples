namespace Copernicus.Core.Modules
{
    public abstract class ModuleBase : IModule
    {
        public abstract string Name { get; }
        public abstract void Initialize(IViewManager viewManager);
    }
}
