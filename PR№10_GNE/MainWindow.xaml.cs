using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.ComponentModel;

namespace PR_10_GNE
{
    public partial class MainWindow : Window
    {
        Entities1 context;
        public MainWindow()
        {
            InitializeComponent();
            context = new Entities1();
            ShowTable();
        }
        public void ShowTable()
        {
            DataGridExhibits.ItemsSource = context.Exhibits.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newExhibits = new Exhibits();
            context.Exhibits.Add(newExhibits);
            var win = new Window1(context, newExhibits);
            win.ShowDialog();
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = DataGridExhibits.SelectedItem as Exhibits;
            if (row==null)
            {
                MessageBox.Show("Сначала выберите строку для удаления");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту строку данных?", "Подтверждение удаления", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                context.Exhibits.Remove(row);
                context.SaveChanges();
                ShowTable();
            }
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btnEdit = sender as Button;
            var currentExhibits = btnEdit.DataContext as Exhibits;
            var win = new Window1(context, currentExhibits);
            win.ShowDialog();
        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxName.Text == null)
            {
                DataGridExhibits.ItemsSource = context.Exhibits.ToList();
            }
            else
            {
                var sel = context.Exhibits.Where(n => n.name.Contains(TextBoxName.Text)).ToList();
                DataGridExhibits.ItemsSource = sel;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BSort_Click(object sender, RoutedEventArgs e)
        {
            DataGridExhibits.ItemsSource = context.Exhibits.ToList();
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(DataGridExhibits.ItemsSource);
            collectionView.SortDescriptions.Add(new SortDescription("age", ListSortDirection.Ascending));
        }
        private void BSort1_Click(object sender, RoutedEventArgs e)
        {
            DataGridExhibits.ItemsSource = context.Exhibits.ToList();
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(DataGridExhibits.ItemsSource);
            collectionView.SortDescriptions.Add(new SortDescription("age", ListSortDirection.Descending));
        }
    }
}
