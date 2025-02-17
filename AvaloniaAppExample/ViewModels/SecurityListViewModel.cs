using AvaloniaAppExample.Services;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace AvaloniaAppExample.ViewModels
{
    public class SecurityListViewModel : ViewModelBase, IDisposable
    {
        private readonly ReadOnlyObservableCollection<SecurityViewModel> _items;
        //private readonly IDisposable _cleanUp;
        public SecurityListViewModel(ISecurityService securityService)
        {
            securityService.Securities
                // Transform in DynamicData works like Select in
                // LINQ, it observes changes in one collection, and
                // projects it's elements to another collection.
                .Transform(x => new SecurityViewModel(x))
                // Filter is basically the same as .Where() operator
                // from LINQ. See all operators in DynamicData docs.
                .Filter(x => true)
                // Ensure the updates arrive on the UI thread.
                .ObserveOn(RxApp.MainThreadScheduler)
                // We .Bind() and now our mutable Items collection 
                // contains the new items and the GUI gets refreshed.
                .Bind(out _items)
                .Subscribe();
        }

        private string _securityName = "";

        public string SecurityName
        {
            get => _securityName;
            set
            {
                this.RaiseAndSetIfChanged(ref _securityName, value);
            }
        }

        public ReadOnlyObservableCollection<SecurityViewModel> Items => _items;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            //_cleanUp.Dispose();
        }
    }
}
