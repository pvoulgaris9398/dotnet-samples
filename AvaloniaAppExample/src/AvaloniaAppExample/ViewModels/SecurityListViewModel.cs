using AvaloniaAppExample.Services;

namespace AvaloniaAppExample.ViewModels
{
    public sealed class SecurityListViewModel : ViewModelBase, IDisposable
    {
        private readonly ReadOnlyObservableCollection<SecurityViewModel> _items;
        private readonly IDisposable _disposable;

        public SecurityListViewModel(ISecurityService securityService)
        {
            ArgumentNullException.ThrowIfNull(securityService, nameof(securityService));
            Service = securityService;
            _items = new([]);
            _disposable = securityService
                .Securities.Transform(x => new SecurityViewModel(x))
                .Filter(x => true)
                //.ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _items)
                .Subscribe();
        }

        public ISecurityService Service { get; }

        public string SecurityName
        {
            get;
            set => this.RaiseAndSetIfChanged(ref field, value);
        } = "";

        public ReadOnlyObservableCollection<SecurityViewModel> Items => _items;

        public void Dispose()
        {
            _disposable.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
