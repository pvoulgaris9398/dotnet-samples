using AvaloniaAppExample.Services;
using AvaloniaAppExample.ViewModels;

namespace AvaloniaAppExample.Views
{
    public partial class PriceListView : ReactiveUserControl<PriceListViewModel>
    {
        public PriceListView()
        {
#pragma warning disable CA2000 // Dispose objects before losing scope
            ViewModel = new PriceListViewModel(new PriceService());
#pragma warning restore CA2000 // Dispose objects before losing scope
            AvaloniaXamlLoader.Load(this);
        }
    }
}
