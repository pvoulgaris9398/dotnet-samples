using AvaloniaAppExample.Services;
using AvaloniaAppExample.ViewModels;

namespace AvaloniaAppExample.Views
{
    public partial class PriceListView : ReactiveUserControl<PriceListViewModel>
    {
        public PriceListView()
        {
            InitializeComponent();
            ViewModel = new PriceListViewModel(
                new PriceService(
                    uiRefreshInterval: TimeSpan.FromMilliseconds(100),
                    maxBatchSize: 1000
                )
            );
        }
    }
}
