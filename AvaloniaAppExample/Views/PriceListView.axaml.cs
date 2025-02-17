using Avalonia.ReactiveUI;
using AvaloniaAppExample.Services;
using AvaloniaAppExample.ViewModels;

namespace AvaloniaAppExample.Views;

public partial class PriceListView : ReactiveUserControl<PriceListViewModel>
{
    public PriceListView()
    {
        InitializeComponent();
        ViewModel = new PriceListViewModel(new PriceService());
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