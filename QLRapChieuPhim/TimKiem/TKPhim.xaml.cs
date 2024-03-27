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

        void Header()
        {
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
        void LoadData()
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
                    P.noiDungC 
                    FROM tblPhim AS P
                    LEFT JOIN tblTheLoai AS T ON P.maTheLoai = T.maTheLoai
                    LEFT JOIN tblQGsanXuat AS Q ON P.maQGSanXuat = Q.maQGSanXuat
                    LEFT JOIN tblHangSX AS H ON P.maHangSX = H.maHangSX";

            DataTable dtPhim = dtBase.ReadData(sql);
            dtgTKPhim.ItemsSource = dtPhim.AsDataView();
            Header();
        }

        private void txtFindAll_TextChanged(object sender, TextChangedEventArgs e)
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

            if (!string.IsNullOrEmpty(txtFindAll.Text.Trim()))
            {
                sql += " AND (P.maPhim LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.tenPhim LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "Q.tenQGSanXuat LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "H.tenHangSX LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.daoDien LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "T.tenTheLoai LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.ngayKhoiChieu LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.ngayKetThuc LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.nuDVC LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "P.namDVC LIKE '%" + txtFindAll.Text + "%')";
            }

            DataTable dtTimKiem = dtBase.ReadData(sql);
            dtgTKPhim.ItemsSource = dtTimKiem.AsDataView();
            Header();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dtTheloai = dtBase.ReadData("Select * from tblTheLoai");
            cboTheloai.ItemsSource = dtTheloai.AsDataView();
            cboTheloai.DisplayMemberPath = "tenTheLoai";
            cboTheloai.SelectedValuePath = "maTheLoai";
            LoadData();
            LoadDataAndSetupComboBox();
        }

        private void cboTheloai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTheloai.SelectedItem != null)
            {
                DataRowView selectedRow = cboTheloai.SelectedItem as DataRowView;
                string selectedTheLoai = selectedRow["tenTheLoai"].ToString();
                SearchByTheLoai(selectedTheLoai);
            }
        }

        private void SearchByTheLoai(string tenTheLoai)
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

            if (!string.IsNullOrEmpty(tenTheLoai))
            {
                sql += " AND tenTheLoai LIKE '%" + tenTheLoai + "%'";
            }

            DataTable dtTimKiem = dtBase.ReadData(sql);
            dtgTKPhim.ItemsSource = dtTimKiem.AsDataView();
            Header();
        }

        private void LoadDataAndSetupComboBox()
        {

            LoadTheLoaiComboBox();
            cboTheloai.SelectionChanged += cboTheloai_SelectionChanged;
            LoadHangSXComboBox();
            cboHangSX.SelectionChanged += cboHangSX_SelectionChanged;
        }

        private void LoadTheLoaiComboBox()
        {
            DataTable dtTheLoai = dtBase.ReadData("SELECT * FROM tblTheLoai");
            cboTheloai.DisplayMemberPath = "tenTheLoai";
            cboTheloai.ItemsSource = dtTheLoai.DefaultView;
        }

        private void cboHangSX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboHangSX.SelectedItem != null)
            {
                DataRowView selectedRow = cboHangSX.SelectedItem as DataRowView;
                string selectedHangSX = selectedRow["tenHangSX"].ToString();
                SearchByHangSX(selectedHangSX);
            }
        }
        private void SearchByHangSX(string tenHangSX)
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

            if (!string.IsNullOrEmpty(tenHangSX))
            {
                sql += " AND tenHangSX LIKE '%" + tenHangSX + "%'";
            }

            DataTable dtTimKiem = dtBase.ReadData(sql);
            dtgTKPhim.ItemsSource = dtTimKiem.AsDataView();
            Header();
        }
        private void LoadHangSXComboBox()
        {
            DataTable dtHangSX = dtBase.ReadData("SELECT * FROM tblHangSX");
            cboHangSX.DisplayMemberPath = "tenHangSX";
            cboHangSX.ItemsSource = dtHangSX.DefaultView;
        }
    }
}
