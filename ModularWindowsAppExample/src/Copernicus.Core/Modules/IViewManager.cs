namespace Copernicus.Core.Modules
{
    public interface IViewManager
    {
        void Show(string title, string message);
        void AddView(string caption, object viewToAdd);
    }
}
