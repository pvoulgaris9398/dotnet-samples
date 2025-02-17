using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaAppExample.Services;
using AvaloniaAppExample.ViewModels;

namespace AvaloniaAppExample.Views;

public partial class SecurityListView : ReactiveUserControl<SecurityListViewModel>
{
    public SecurityListView()
    {
        ViewModel = new SecurityListViewModel(new SecurityService());
        AvaloniaXamlLoader.Load(this);
    }

    //public void OnClick(object sender, RoutedEventArgs args)
    //{
    //    var button = myButton;
    //    if (DataContext is SecurityListViewModel lvm)
    //    {
    //        lvm.SecurityName = "IBM US";
    //    }
    //}
}