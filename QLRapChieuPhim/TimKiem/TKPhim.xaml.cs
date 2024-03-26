using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using QLRapChieuPhim.Classes;

namespace QLRapChieuPhim.TimKiem
{
    /// <summary>
    /// Interaction logic for TKPhim.xaml
    /// </summary>
    public partial class TKPhim : Window
    {
        Classes.DataProcessor dtBase = new DataProcessor(Login.cinemaID);
        public TKPhim()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            string sql = @"SELECT 
                        P.maPhim, 
                        P.tenPhim, 
                        Q.tenQGSanXuat AS TenQuocGia, 
                        P.maHangSX, 
                        P.daoDien, 
                        T.tenTheLoai AS TenTheLoai, 
                        P.ngayKhoiChieu, 
                        P.ngayKetThuc, 
                        P.nuDVC, 
                        P.namDVC, 
                        P.noiDungC 
                    FROM tblPhim AS P
                    LEFT JOIN tblTheLoai AS T ON P.maTheLoai = T.maTheLoai
                    LEFT JOIN tblQGSanXuat AS Q ON P.maQGSanXuat = Q.maQGSanXuat";

            DataTable dtPhim = dtBase.ReadData(sql);
            dtgTKPhim.ItemsSource = dtPhim.AsDataView();
            dtgTKPhim.Columns[0].Header = "Mã Phim";
            dtgTKPhim.Columns[1].Header = "Tên Phim";
            dtgTKPhim.Columns[2].Header = "Quốc gia sản xuất";
            dtgTKPhim.Columns[3].Header = "Hãng sản xuất";
            dtgTKPhim.Columns[4].Header = "Đạo diễn";
            dtgTKPhim.Columns[5].Header = "Thể loại";
            dtgTKPhim.Columns[6].Header = "Ngày khởi chiếu";
            dtgTKPhim.Columns[7].Header = "Ngày kết thúc ";
            dtgTKPhim.Columns[8].Header = "Nữ diễn viên chính";
            dtgTKPhim.Columns[9].Header = "Nam diễn viên chính";
            dtgTKPhim.Columns[10].Header = "Nội dung chính";
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {

            string sql = @"SELECT P.maPhim, 
                          P.tenPhim, 
                          Q.tenQGSanXuat AS TenQuocGia, 
                          P.maHangSX, 
                          P.daoDien, 
                          T.tenTheLoai AS TenTheLoai, 
                          P.ngayKhoiChieu, 
                          P.ngayKetThuc, 
                          P.nuDVC, 
                          P.namDVC, 
                          P.noiDungC 
                   FROM tblPhim AS P
                   LEFT JOIN tblTheLoai AS T ON P.maTheLoai = T.maTheLoai
                   LEFT JOIN tblQGSanXuat AS Q ON P.maQGSanXuat = Q.maQGSanXuat
                   WHERE 1=1";

            if (!string.IsNullOrEmpty(txtFindAll.Text.Trim()))
            {
                sql += " AND (P.maPhim LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.tenPhim LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "Q.tenQGSanXuat LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.maHangSX LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.daoDien LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "T.tenTheLoai LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.ngayKhoiChieu LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.ngayKetThuc LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.nuDVC LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.namDVC LIKE '%" + txtFindAll.Text + "%')";
            }

            DataTable dtTimKiem = dtBase.ReadData(sql);
            dtgTKPhim.ItemsSource = dtTimKiem.AsDataView();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
