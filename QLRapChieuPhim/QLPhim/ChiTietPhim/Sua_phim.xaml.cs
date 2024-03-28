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

namespace QLRapChieuPhim.QLPhim.ChiTietPhim
{
    /// <summary>
    /// Interaction logic for Sua_phim.xaml
    /// </summary>
    public partial class Sua_phim : Window
    {
        public Sua_phim()
        {
            InitializeComponent();
        }

        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);

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
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Lưu dữ liệu vào cơ sở dữ liệu
            try
            {
                if (!string.IsNullOrWhiteSpace(txtID.Text) && !string.IsNullOrWhiteSpace(txtTenphim.Text) && cboQuocgia.SelectedItem.ToString() != null && cboHangSX.SelectedItem.ToString() != null && !string.IsNullOrWhiteSpace(txtDaodien.Text) && cboTheLoai.SelectedItem.ToString() != null && !string.IsNullOrWhiteSpace(txtNgayKC.Text) && !string.IsNullOrWhiteSpace(txtNgayKT.Text) && !string.IsNullOrWhiteSpace(txtNuDVC.Text) && !string.IsNullOrWhiteSpace(txtNamDVC.Text) && !string.IsNullOrWhiteSpace(txtNoidung.Text) && !string.IsNullOrWhiteSpace(txtChiPhi.Text) && !string.IsNullOrWhiteSpace(txtThu.Text))
                {
                    dataProcessor.ChangeData("UPDATE tblPhim SET tenPhim = '" + txtTenphim.Text + "',maQGsanXuat = '" + cboQuocgia.SelectedValue + "', maHangSX = '" + cboHangSX.SelectedValue + "', daoDien = '" + txtDaodien.Text + "',maTheLoai = '" + cboTheLoai.SelectedValue + "',ngayKhoiChieu = '" + txtNgayKC.Text + "', ngayKetThuc = '" + txtNgayKT.Text + "', nuDVC = '" + txtNuDVC.Text + "', namDVC = '" + txtNamDVC.Text + "', noiDungC = '" + txtNoidung.Text + "',tongChiPhi = '" + int.Parse(txtChiPhi.Text) + "', tongThu = '" + int.Parse(txtThu.Text) + "'WHERE maPhim = '" + txtID.Text + "'");

                }
                MessageBox.Show("Đã cập nhật thông tin phim thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // cbo Quốc gia sản xuất
            DataTable dtQuocgia = dataProcessor.ReadData("Select * from tblQGsanXuat");
            cboQuocgia.ItemsSource = dtQuocgia.AsDataView();
            cboQuocgia.DisplayMemberPath = "tenQGSanXuat";
            cboQuocgia.SelectedValuePath = "maQGSanXuat";

            //cbo Hãng sản xuất
            DataTable dtHangSX = dataProcessor.ReadData("Select * from tblHangSX");
            cboHangSX.ItemsSource = dtHangSX.AsDataView();
            cboHangSX.DisplayMemberPath = "tenHangSX";
            cboHangSX.SelectedValuePath = "maHangSX";

            //cbo Thể loại
            DataTable dtTheLoai = dataProcessor.ReadData("Select * from tblTheLoai");
            cboTheLoai.ItemsSource = dtTheLoai.AsDataView();
            cboTheLoai.DisplayMemberPath = "tenTheLoai";
            cboTheLoai.SelectedValuePath = "maTheLoai";
            LoadData();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void dgPhim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPhim.SelectedItem != null)
            {
                DataRowView selectedRow = dgPhim.SelectedItem as DataRowView;
                txtID.IsEnabled = false;

                // Hiển thị thông tin từ dòng đã chọn lên 
                if (selectedRow != null)
                {
                    txtID.Text = selectedRow["maPhim"].ToString();
                    txtTenphim.Text = selectedRow["tenPhim"].ToString();
                    txtDaodien.Text = selectedRow["daoDien"].ToString();
                    txtNgayKC.Text = selectedRow["ngayKhoiChieu"].ToString();
                    txtNgayKT.Text = selectedRow["ngayKetThuc"].ToString();
                    txtNuDVC.Text = selectedRow["nuDVC"].ToString();
                    txtNamDVC.Text = selectedRow["namDVC"].ToString();
                    txtNoidung.Text = selectedRow["noiDungC"].ToString();
                    txtChiPhi.Text = selectedRow["tongChiPhi"].ToString();
                    txtThu.Text = selectedRow["tongThu"].ToString();

                    
                    string selectedQG = selectedRow["TenQuocGia"].ToString();
                    cboQuocgia.Text = selectedQG;
                    string selectedHSX = selectedRow["TenHangSX"].ToString();
                    cboHangSX.Text = selectedHSX;
                    string selectedTL = selectedRow["TenTheLoai"].ToString();
                    cboTheLoai.Text = selectedTL;
                }
                else
                {
                    ClearFields();
                    txtID.IsEnabled = true;
                }

            }
            else
            {
                ClearFields();
                txtID.IsEnabled = true;
            }

            void ClearFields()
            {
                txtID.Text = "";
                txtTenphim.Text = "";
                txtDaodien.Text = "";
                txtNgayKC.Text = "";
                txtNgayKT.Text = "";
                txtNuDVC.Text = "";
                txtNamDVC.Text = "";
                txtNoidung.Text = "";
                txtChiPhi.Text = "";
                txtThu.Text = "";

                cboQuocgia.SelectedItem = null;
                cboHangSX.SelectedItem = null;
                cboTheLoai.SelectedItem = null;

            }
        }
    }
}
