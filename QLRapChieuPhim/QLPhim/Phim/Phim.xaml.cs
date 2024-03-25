using QLRapChieuPhim.Classes;
using QLRapChieuPhim.QLPhim.Phim;
using QLRapChieuPhim.QLPhim.Phim.Edit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Shell;
using System.Windows.Threading;


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
           
            dataProcessor = new DataProcessor(Login.cinemaID);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = dataProcessor.ReadData("SELECT * FROM tblPhim");
                dgPhim.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
           
            
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
     


        }
    }
}
