using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaAppExample.ViewModels;

namespace AvaloniaAppExample.Views
{
    public partial class SandboxView : ReactiveUserControl<SandboxViewModel>
    {
        public SandboxView()
        {
            //            InitializeComponent();
            //#if DEBUG
            //            this.AttachDevTools();
            //#endif
            ViewModel = new SandboxViewModel();
            AvaloniaXamlLoader.Load(this);
            //TheButton.Content = "testing";
        }
    }
}