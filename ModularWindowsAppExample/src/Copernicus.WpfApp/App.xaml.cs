using System.Windows;

using Copernicus.Core.Modules;

namespace Copernicus.WpfApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ModuleManager _moduleManager;
    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Loaded += MainWindow_Loaded;
        var _moduleManager = new ModuleManager(mainWindow);
        mainWindow.Tag = _moduleManager;
        _moduleManager.Load(mainWindow);
        base.OnStartup(e);
        mainWindow.Show();
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is MainWindow mw &&
            mw.Tag is ModuleManager mm)
        {
            mm.Unload();
        }
    }
}

