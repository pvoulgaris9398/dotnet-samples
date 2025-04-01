using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaAppExample.Services;
using AvaloniaAppExample.ViewModels;

namespace AvaloniaAppExample.Views
{
    public partial class PriceListView : ReactiveUserControl<PriceListViewModel>
    {
        public PriceListView()
        {
            ViewModel = new PriceListViewModel(new PriceService());
            AvaloniaXamlLoader.Load(this);
        }

        public void ClickHandler(object sender, RoutedEventArgs args)
        {

        }
    }
}