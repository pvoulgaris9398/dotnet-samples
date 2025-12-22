namespace AvaloniaAppExample.Views
{
    public partial class MainWindow2 : Window
    {
        public MainWindow2()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        /// <summary>
        /// This is, of course, a temporary hack...
        /// </summary>
        public void OnClick1(object sender, RoutedEventArgs e) =>
            content.Content = new SecurityListView();

        public void OnClick2(object sender, RoutedEventArgs e) =>
            content.Content = new PriceListView();

        public void OnClick3(object sender, RoutedEventArgs e) =>
            content.Content = new SandboxView();
    }
}
