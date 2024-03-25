using QLRapChieuPhim.QLPhim;
using QLRapChieuPhim.QLPhim.Hang_SX;
using QLRapChieuPhim.QLPhim.QuocGia_Sx;
using QLRapChieuPhim.QLPhim.The_Loai;
using QLRapChieuPhim.QLRap;
using QLRapChieuPhim.QLRap.Gio_Chieu;
using QLRapChieuPhim.QLRap.Lich_Chieu;
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

namespace QLRapChieuPhim
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {

        public HomePage()
        {
            InitializeComponent();

            this.WindowStyle = WindowStyle.None;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát ứng dụng?", "Thong bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }


        private void mnuPhongChieu_Click(object sender, RoutedEventArgs e)
        {
            Phong_chieu phong_Chieu = new Phong_chieu();
            phong_Chieu.ShowDialog();
        }

        private void mnuLichChieu_Click(object sender, RoutedEventArgs e)
        {
            Lich_chieu Lich_chieu = new Lich_chieu();
            Lich_chieu.ShowDialog();
        }

        private void mnuGioChieu_Click(object sender, RoutedEventArgs e)
        {
            Gio_chieu Gio_chieu = new Gio_chieu();
            Gio_chieu.ShowDialog();
        }


        private void mnuTheLoai_Click(object sender, RoutedEventArgs e)
        {
            The_loai The_loai = new The_loai();
            The_loai.ShowDialog();
        }

        private void mnuHangSX_Click(object sender, RoutedEventArgs e)
        {
            Hang_sx Hang_sx = new Hang_sx();
            Hang_sx.ShowDialog();
        }

        private void mnuQuocGiaSX_Click(object sender, RoutedEventArgs e)
        {
            QuocGia_sx QuocGia_sx = new QuocGia_sx();
            QuocGia_sx.ShowDialog();
        }
    }
}
