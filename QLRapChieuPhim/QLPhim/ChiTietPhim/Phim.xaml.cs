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
            LoadData();
        }
        private void LoadData()
        {
            string sql = @"SELECT 
                    P.maPhim, 
                    P.tenPhim, 
                    Q.tenQGSanXuat AS TenQuocGia, 
                    H.tenHangSX AS TenHangSX, 
                    P.daoDien, 
                    T.tenTheLoai AS TenTheLoai, 
                    P.ngayKhoiChieu, 
                    P.ngayKetThuc, 
                    P.nuDVC, 
                    P.namDVC, 
                    P.noiDungC,
                    P.tongChiPhi,
                    P.tongThu
                    FROM tblPhim AS P
                    LEFT JOIN tblTheLoai AS T ON P.maTheLoai = T.maTheLoai
                    LEFT JOIN tblQGsanXuat AS Q ON P.maQGSanXuat = Q.maQGSanXuat
                    LEFT JOIN tblHangSX AS H ON P.maHangSX = H.maHangSX";

            DataTable dtPhim = dataProcessor.ReadData(sql);
            dgPhim.ItemsSource = dtPhim.AsDataView();
            Header();
        }
        void Header()
        {
            dgPhim.Columns[0].Header = "Mã Phim";
            dgPhim.Columns[1].Header = "Tên Phim";
            dgPhim.Columns[2].Header = "Quốc gia sản xuất";
            dgPhim.Columns[3].Header = "Hãng sản xuất";
            dgPhim.Columns[4].Header = "Đạo diễn";
            dgPhim.Columns[5].Header = "Thể loại";
            dgPhim.Columns[6].Header = "Ngày khởi chiếu";
            dgPhim.Columns[7].Header = "Ngày kết thúc ";
            dgPhim.Columns[8].Header = "Nữ diễn viên chính";
            dgPhim.Columns[9].Header = "Nam diễn viên chính";
            dgPhim.Columns[10].Header = "Nội dung chính";
            dgPhim.Columns[11].Header = "Tổng chi phí";
            dgPhim.Columns[12].Header = "Tổng thu";
        }
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            Them_phim them_Phim = new Them_phim();
            them_Phim.ShowDialog();
        }


        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            Sua_phim suaPhim = new Sua_phim();
            suaPhim.ShowDialog();
            RefreshDataGrid();
        }
    


       private void btnXoa_Click(object sender, RoutedEventArgs e)
{
    // Kiểm tra xem có dòng nào được chọn không
    if (dgPhim.SelectedItem != null)
    {
        // Lấy dòng được chọn từ DataGrid
        DataRowView selectedRow = dgPhim.SelectedItem as DataRowView;

        // Kiểm tra xem dòng được chọn có null không
        if (selectedRow != null)
        {
            // Lấy giá trị của cột "maPhim" từ dòng được chọn
            string maPhim = selectedRow["maPhim"].ToString();

            // Hiển thị hộp thoại xác nhận xóa
            if (MessageBox.Show("Bạn có muốn xóa phim này không ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Thực hiện lệnh xóa dữ liệu từ cơ sở dữ liệu
                dataProcessor.ChangeData("DELETE FROM tblPhim WHERE maPhim = '" + maPhim + "'");
                
                // Refresh DataGrid sau khi xóa dữ liệu
                LoadData();
            }
        }
    }
    else
    {
        MessageBox.Show("Vui lòng chọn một phim để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát ứng dụng?", "Thong bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void txtFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            string sql = @"SELECT P.maPhim, 
                  P.tenPhim, 
                  Q.tenQGSanXuat AS TenQuocGia, 
                  H.tenHangSX AS TenHangSX, 
                  P.daoDien, 
                  T.tenTheLoai AS TenTheLoai, 
                  P.ngayKhoiChieu, 
                  P.ngayKetThuc, 
                  P.nuDVC, 
                  P.namDVC, 
                  P.noiDungC 
                   FROM tblPhim AS P
                   LEFT JOIN tblTheLoai AS T ON P.maTheLoai = T.maTheLoai
                   LEFT JOIN tblQGsanXuat AS Q ON P.maQGSanXuat = Q.maQGsanXuat
                   LEFT JOIN tblHangSX AS H ON P.maHangSX = H.maHangSX
                   WHERE 1=1";

            if (!string.IsNullOrEmpty(txtFind.Text.Trim()))
            {
                sql += " AND (P.maPhim LIKE '%" + txtFind.Text + "%' OR ";
                sql += "P.tenPhim LIKE '%" + txtFind.Text + "%' OR ";
                sql += "Q.tenQGSanXuat LIKE '%" + txtFind.Text + "%' OR ";
                sql += "H.tenHangSX LIKE '%" + txtFind.Text + "%' OR ";
                sql += "P.daoDien LIKE '%" + txtFind.Text + "%' OR ";
                sql += "T.tenTheLoai LIKE '%" + txtFind.Text + "%' OR ";
                sql += "P.ngayKhoiChieu LIKE '%" + txtFind.Text + "%' OR ";
                sql += "P.ngayKetThuc LIKE '%" + txtFind.Text + "%' OR ";
                sql += "P.nuDVC LIKE '%" + txtFind.Text + "%' OR ";
                sql += "P.namDVC LIKE '%" + txtFind.Text + "%')";
            }

            DataTable dtTimKiem = dataProcessor.ReadData(sql);
            dgPhim.ItemsSource = dtTimKiem.AsDataView();
            Header();
        }
    }

}
