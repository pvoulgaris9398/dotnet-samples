﻿using System.Diagnostics;
using System.Windows.Controls;

using Copernicus.Core.Interfaces;
namespace Copernicus.Core.Controls
{
    /// <summary>
    /// Interaction logic for EnhancedDataGrid.xaml
    /// </summary>
    public partial class EnhancedDataGrid : DataGrid
    {
        public EnhancedDataGrid()
        {
            InitializeComponent();

            Loaded += OnLoaded;

        }

        ~EnhancedDataGrid()
        {
            Loaded -= OnLoaded;
        }

        // TODO: Move static to another class???
        private static void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Debug.WriteLine(nameof(OnLoaded));

            if (sender is EnhancedDataGrid grid &&
                grid.DataContext is ICanRefresh lvm &&
                lvm.CanRefresh &&
                lvm.MayRefresh)
            {
                lvm.Refresh();
            }
        }

        protected override void OnAddingNewItem(AddingNewItemEventArgs e)
        {
            Debug.WriteLine(nameof(OnAddingNewItem));

            if (DataContext is ICanCreateNewItem lvm)
            {
                var newItem = lvm.Create();
                e.NewItem = newItem;
            }
        }

        protected override void OnInitializingNewItem(InitializingNewItemEventArgs e)
        {
            Debug.WriteLine(nameof(OnInitializingNewItem));

            /*
             * TODO: Implement callback into ListViewModel to get styles, etc.
             */
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {

            Debug.WriteLine(nameof(OnSelectionChanged));

            var added = e.AddedItems;
            var removed = e.RemovedItems;
            var source = e.Source;
            var originalSource = e.OriginalSource;

        }
    }
}
