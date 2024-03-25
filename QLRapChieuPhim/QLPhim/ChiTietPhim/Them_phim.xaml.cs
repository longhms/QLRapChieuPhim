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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QLRapChieuPhim.QLPhim.ChiTietPhim
{
    /// <summary>
    /// Interaction logic for Them_phim.xaml
    /// </summary>
    public partial class Them_phim : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        public Them_phim(DataRowView selectedRow)
        {
            InitializeComponent();
            txtID.Text = selectedRow["maPhim"].ToString();
            txtTenphim.Text = selectedRow["tenPhim"].ToString();
            cboQuocgia.SelectedItem = selectedRow["maQGSanXuat"].ToString();
            cboHangSX.SelectedItem = selectedRow["maHangSX"].ToString();
            txtDaodien.Text = selectedRow["daoDien"].ToString();
            cboTheLoai.SelectedItem = selectedRow["maTheloai"].ToString();
            txtNgayKC.Text = selectedRow["ngayKhoiChieu"].ToString();
            txtNgayKT.Text = selectedRow["ngayKetThuc"].ToString();
            txtNuDVC.Text = selectedRow["nuDVC"].ToString();
            txtNamDVC.Text = selectedRow["namDVC"].ToString();
            txtNoidung.Text = selectedRow["noiDungC"].ToString();
            txtChiPhi.Text = selectedRow["tongChiPhi"].ToString();
            txtThu.Text = selectedRow["tongThu"].ToString();

        }

        public Them_phim()
        {
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
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtPhim = new DataTable();
            if(txtID.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã phim");
                txtID.Focus();
                return;
            }
            //Kiem tra trung ma
            dtPhim = dataProcessor.ReadData("Select * from tblPhim where maPhim = '" + txtID.Text + "'");
            if(dtPhim.Rows.Count > 0)
            {
                MessageBox.Show("Mã phim bị trùng, vui lòng nhập mã khác");
                txtID.Focus();
                return;
            }
            dataProcessor.ChangeData("insert into tblPhim values('" + txtID.Text + "','" + txtTenphim.Text + "','" + cboQuocgia.SelectedItem.ToString() + "','" + cboHangSX.SelectedItem.ToString() + "','" + txtDaodien.Text + "','" + cboTheLoai.SelectedItem.ToString() + "','" + txtNgayKC.Text + "','" + txtNgayKT.Text + "','" + txtNuDVC.Text + "','" + txtNamDVC.Text + "','" + txtNoidung.Text + "','" + txtChiPhi + "','" + txtThu.Text + "') ");
            MessageBox.Show("Thêm thành công");

            try
            {
                // Lưu dữ liệu vào cơ sở dữ liệu

                // Gọi phương thức cập nhật dữ liệu trên cửa sổ Phim
                Phim phim = Application.Current.Windows.OfType<Phim>().FirstOrDefault();
                if (phim != null)
                {
                    phim.RefreshDataGrid();
                }

                // Hiển thị thông báo lưu thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi lưu dữ liệu
            }
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
            
             if (!string.IsNullOrWhiteSpace(txtID.Text) && !string.IsNullOrWhiteSpace(txtTenphim.Text) && cboQuocgia.SelectedItem != null && cboHangSX.SelectedItem != null && !string.IsNullOrWhiteSpace(txtDaodien.Text) && cboTheLoai.SelectedItem != null && !string.IsNullOrWhiteSpace(txtNgayKC.Text) && !string.IsNullOrWhiteSpace(txtNgayKT.Text) && !string.IsNullOrWhiteSpace(txtNuDVC.Text) && !string.IsNullOrWhiteSpace(txtNamDVC.Text) && !string.IsNullOrWhiteSpace(txtNoidung.Text) && !string.IsNullOrWhiteSpace(txtChiPhi.Text) && !string.IsNullOrWhiteSpace(txtThu.Text))
             {
                dataProcessor.ChangeData("UPDATE tblPhim SET tenPhim = '" + txtTenphim.Text + "', maQGsanXuat = '" + cboQuocgia.SelectedValue + "', maHangSX = '" + cboHangSX.SelectedValue + "', daoDien = '" + txtDaodien.Text + "', maTheloai = '" + cboTheLoai.SelectedValue + "', ngayKhoiChieu = '" + txtNgayKC.Text + "', ngayKetThuc = '" + txtNgayKT.Text + "', nuDVC = '" + txtNuDVC.Text + "', namDVC = '" + txtNamDVC.Text + "', noiDungC = '" + txtNoidung.Text + "', tongChiPhi = " + txtChiPhi.Text + ", tongThu = " + txtThu.Text + " WHERE maPhim = '" + txtID.Text + "'");
               
             }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin giáo viên cần sửa.", "Thông báo");
            }
            try
            {
                // Lưu dữ liệu vào cơ sở dữ liệu

                // Gọi phương thức cập nhật dữ liệu trên cửa sổ Phim
                Phim phim = Application.Current.Windows.OfType<Phim>().FirstOrDefault();
                if (phim != null)
                {
                    phim.RefreshDataGrid();
                }

                // Hiển thị thông báo lưu thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi lưu dữ liệu
            }
        }
    }
}
