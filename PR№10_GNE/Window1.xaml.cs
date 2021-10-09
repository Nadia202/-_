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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;


namespace PR_10_GNE
{
    public partial class Window1 : Window
    {
        Entities1 context;
        public Window1(Entities1 context, Exhibits exhibits)
        {
            InitializeComponent();
            this.context = context;
            ComboBoxHall.ItemsSource = context.Halls.ToList();
            this.DataContext = exhibits;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveImage();
            context.SaveChanges();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ShowTable();
            this.Close();
        }
        private void SaveImage()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image files: *.jpg, *.png|*.jpg;*.png";
            openFile.ShowDialog();
            if (openFile.FileName.Length !=0)
            {
                string nameFile = openFile.FileName;
                byte[] image = File.ReadAllBytes(nameFile);
                var reg = (Exhibits)this.DataContext;
                reg.imade = image;
                imade.Source = new BitmapImage(new Uri(nameFile));
            }
        }
    }
}
