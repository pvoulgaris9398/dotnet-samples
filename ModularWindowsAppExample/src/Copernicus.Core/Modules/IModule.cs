namespace Copernicus.Core.Modules
{
    public interface IModule
    {
        string Name { get; }
        void Initialize(IViewManager viewManager);
    }
}
