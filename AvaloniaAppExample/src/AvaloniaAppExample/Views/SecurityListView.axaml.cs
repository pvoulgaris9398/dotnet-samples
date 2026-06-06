using AvaloniaAppExample.Services;
using AvaloniaAppExample.ViewModels;

namespace AvaloniaAppExample.Views
{
    public partial class SecurityListView : ReactiveUserControl<SecurityListViewModel>
    {
        public SecurityListView()
        {
#pragma warning disable CA2000 // Dispose objects before losing scope
            ViewModel = new SecurityListViewModel(new SecurityService());
#pragma warning restore CA2000 // Dispose objects before losing scope
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
