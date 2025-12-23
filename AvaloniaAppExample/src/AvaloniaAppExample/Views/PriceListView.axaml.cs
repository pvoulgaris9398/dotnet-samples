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
    }
}
