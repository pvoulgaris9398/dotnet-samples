using System.Windows;
using System.Windows.Controls;
using Copernicus.Core.Modules;

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

    public void AddView(string caption, object viewToAdd)
    {
        if (viewToAdd is UIElement view)
        {
            var item = new TabItem { Header = caption, Content = view };
            DockingArea.Items.Add(item);
        }
    }
}
