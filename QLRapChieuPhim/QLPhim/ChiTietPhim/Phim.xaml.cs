using QLRapChieuPhim.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QLRapChieuPhim.QLPhim.ChiTietPhim
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
        public void RefreshDataGrid()
        {
            // Gọi lại phương thức LoadData hoặc bất kỳ phương thức nào khác để tải lại dữ liệu từ cơ sở dữ liệu và cập nhật DataGrid
            LoadData();
        }
        public void UpdateDataGrid()
        {
            LoadData(); // Gọi lại phương thức LoadData để cập nhật dữ liệu trên DataGrid
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

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            Them_phim them_Phim = new Them_phim();
            them_Phim.ShowDialog();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (dgPhim.SelectedItem != null)
            {
                // Lấy hàng được chọn từ DataGrid
                DataRowView selectedRow = dgPhim.SelectedItem as DataRowView;

                // Khởi tạo cửa sổ Them_phim và truyền dữ liệu từ hàng được chọn
                Them_phim themPhim = new Them_phim(selectedRow);
                themPhim.ShowDialog();

                // Cập nhật lại DataGrid sau khi chỉnh sửa
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phim để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa phim này không ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DataRowView selectedRow = dgPhim.SelectedItem as DataRowView;

                if (selectedRow != null)
                {
                    string maPhim = selectedRow["maPhim"].ToString();
                    dataProcessor.ChangeData("DELETE FROM tblPhim WHERE maPhim = '" + maPhim + "'");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một phim để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }

}
