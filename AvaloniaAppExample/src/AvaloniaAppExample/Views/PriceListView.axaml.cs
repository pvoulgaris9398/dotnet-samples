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
                /*
                With these settings production and consumption are about equal
                */
                new PriceService(
                    uiRefreshInterval: TimeSpan.FromMilliseconds(100),
                    maxBatchSize: 1000
                )
            );
        }
    }
}
