using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaAppExample.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    public void ClickHandler(object sender, RoutedEventArgs args)
    {
        if (Price1 == null)
        {
            Price2.DataContext = null;
            Price2 = null;
            return;
        }
        Price1.DataContext = null;
        Price1 = null;

    }
}