using AvaloniaAppExample.ViewModels;

namespace AvaloniaAppExample.Views
{
    public partial class SandboxView : ReactiveUserControl<SandboxViewModel>
    {
        public SandboxView()
        {
            ViewModel = new SandboxViewModel();
            AvaloniaXamlLoader.Load(this);
        }
    }
}
