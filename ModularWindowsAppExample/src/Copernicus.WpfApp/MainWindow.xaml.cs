using Copernicus.Core.Modules;
using System.Windows;

namespace Copernicus.WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IViewManager
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void Show(string title, string message)
    {
        MessageBox.Show(title, message);
    }
}