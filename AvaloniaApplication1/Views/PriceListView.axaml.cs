using Avalonia.ReactiveUI;
using AvaloniaApplication1.Services;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views;

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