using Copernicus.Core.Modules;
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
        var moduleManager = new ModuleManager(mainWindow);
        moduleManager.Load();
        base.OnStartup(e);
        mainWindow.Show();
    }
}

