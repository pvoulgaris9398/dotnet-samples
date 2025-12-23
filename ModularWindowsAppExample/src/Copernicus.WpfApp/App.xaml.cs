using System.Windows;

namespace Copernicus.WpfApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Loaded += MainWindow_Loaded;
        var moduleManager = new ModuleManager(mainWindow);
        mainWindow.Tag = moduleManager;
        moduleManager.Load(mainWindow);
        base.OnStartup(e);
        mainWindow.Show();
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is MainWindow mw && mw.Tag is ModuleManager2 mm)
        {
            mm.Unload();
        }
    }
}
