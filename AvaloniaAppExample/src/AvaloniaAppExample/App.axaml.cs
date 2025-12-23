using AvaloniaAppExample.ViewModels;
using AvaloniaAppExample.Views;

namespace AvaloniaAppExample
{
    public partial class App : Application
    {
        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow2 { DataContext = new MainWindowViewModel2() };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
