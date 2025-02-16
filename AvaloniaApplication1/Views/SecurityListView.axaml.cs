using Avalonia.ReactiveUI;
using AvaloniaApplication1.Services;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views;

public partial class SecurityListView : ReactiveUserControl<SecurityListViewModel>
{
    public SecurityListView()
    {
        InitializeComponent();
        ViewModel = new SecurityListViewModel(new SecurityService());
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