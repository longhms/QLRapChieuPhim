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
            DataTable dtPhim = dtBase.ReadData("Select maPhim,tenPhim,maQGSanXuat,maHangSX,daoDien,maTheloai,ngayKhoiChieu,ngayKetThuc,nuDVC,namDVC,noiDungC from tblPhim");
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

            string sql = "SELECT * FROM tblPhim WHERE 1=1";


            if (!string.IsNullOrEmpty(txtFindAll.Text.Trim()))
            {
                sql += " AND (maPhim LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "tenPhim LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "maQGSanXuat LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "maHangSX LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "daoDien LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "maTheLoai LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "ngayKhoiChieu LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "ngayKetThuc LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "nuDVC LIKE '%" + txtFindAll.Text + "%' OR ";
                sql += "namDVC LIKE '%" + txtFindAll.Text + "%')";

            }

            DataTable dtTimKiem = dtBase.ReadData(sql);
            dtgTKPhim.ItemsSource = dtTimKiem.AsDataView();
            LoadData();
        }
    }
}
