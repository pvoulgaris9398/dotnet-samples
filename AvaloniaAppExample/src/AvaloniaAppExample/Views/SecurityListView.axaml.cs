using AvaloniaAppExample.Services;
using AvaloniaAppExample.ViewModels;

namespace AvaloniaAppExample.Views
{
    public partial class SecurityListView : ReactiveUserControl<SecurityListViewModel>
    {
        public SecurityListView()
        {
            ViewModel = new SecurityListViewModel(new SecurityService());
            AvaloniaXamlLoader.Load(this);
        }

        public void Remove(object sender, RoutedEventArgs args)
        {
            if (DataContext is SecurityListViewModel lvm)
            {
                // Hack to trigger some updates
                (lvm.Service as SecurityService)?.RemoveItem();
            }
        }

        public void Add(object sender, RoutedEventArgs args)
        {
            if (DataContext is SecurityListViewModel lvm)
            {
                // Hack to trigger some updates
                (lvm.Service as SecurityService)?.AddItem();
            }
        }
    }
}
