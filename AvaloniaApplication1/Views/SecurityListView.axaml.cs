using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views;

public partial class SecurityListView : UserControl
{
    public SecurityListView()
    {
        InitializeComponent();
    }

    public void OnClick(object sender, RoutedEventArgs args)
    {
        if (DataContext is SecurityListViewModel lvm)
        {
            lvm.Count++;
        }
    }
}