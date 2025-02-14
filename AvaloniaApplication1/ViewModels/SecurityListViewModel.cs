using ReactiveUI;

namespace AvaloniaApplication1.ViewModels
{
    public class SecurityListViewModel : ViewModelBase
    {
        public string Caption
        {
            get
            {
                return Count > 0 ? $"I have been clicked: {Count} time(s)!" : "Security List View";
            }
        }

        private int _count = 0;
        public int Count
        {
            get => _count; set
            {
                this.RaiseAndSetIfChanged(ref _count, value);
                this.RaisePropertyChanged(nameof(Caption));
            }
        }
    }
}
