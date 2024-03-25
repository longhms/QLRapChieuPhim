using QLRapChieuPhim.Classes;
using System;
using System.Collections.Generic;
using System.Data;
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


namespace QLRapChieuPhim.QLPhim
{
    /// <summary>
    /// Interaction logic for Phim.xaml
    /// </summary>
    public partial class Phim : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
       
        public Phim()
        {
            InitializeComponent();
        }

        void Loaddata()
        {
            DataTable dt = dataProcessor.ReadData("SELECT * FROM tblPhim");
            dgPhim.ItemsSource = dt.AsDataView();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
