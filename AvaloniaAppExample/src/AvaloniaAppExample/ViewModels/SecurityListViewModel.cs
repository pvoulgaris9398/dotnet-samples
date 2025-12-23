using AvaloniaAppExample.Services;

namespace AvaloniaAppExample.ViewModels
{
    public class SecurityListViewModel : ViewModelBase, IDisposable
    {
        private readonly ReadOnlyObservableCollection<SecurityViewModel> _items;
        private readonly IDisposable _disposable;

        public SecurityListViewModel(ISecurityService securityService)
        {
            Service = securityService;
            _disposable = securityService
                .Securities.Transform(x => new SecurityViewModel(x))
                .Filter(x => true)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _items)
                .Subscribe();
        }

        public ISecurityService Service { get; }

        private string _securityName = "";

        public string SecurityName
        {
            get => _securityName;
            set => this.RaiseAndSetIfChanged(ref _securityName, value);
        }

        public ReadOnlyObservableCollection<SecurityViewModel> Items => _items;

        public void Dispose()
        {
            _disposable.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
