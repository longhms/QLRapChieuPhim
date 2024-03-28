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
        DataRowView selectedRow;

        public Sua_phim(DataRowView selectedRow)
        {
            InitializeComponent();
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

            // Gọi hàm để gán giá trị cho ComboBox Quocgia
            GanGiaTriComboBox(cboQuocgia, selectedRow["maQGSanXuat"].ToString());

            // Gọi hàm để gán giá trị cho ComboBox HangSX
            GanGiaTriComboBox(cboHangSX, selectedRow["maHangSX"].ToString());

            // Gọi hàm để gán giá trị cho ComboBox TheLoai
            GanGiaTriComboBox(cboTheLoai, selectedRow["maTheLoai"].ToString());


        }
        private void GanGiaTriComboBox(ComboBox comboBox, string maGiaTri)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if (item.Tag.ToString() == maGiaTri)
                {
                    comboBox.SelectedItem = item;
                    break;
                }
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
            try
            {
                if (!string.IsNullOrWhiteSpace(txtID.Text) && !string.IsNullOrWhiteSpace(txtTenphim.Text) && cboQuocgia.SelectedItem != null && cboHangSX.SelectedItem != null && !string.IsNullOrWhiteSpace(txtDaodien.Text) && cboTheLoai.SelectedItem != null && !string.IsNullOrWhiteSpace(txtNgayKC.Text) && !string.IsNullOrWhiteSpace(txtNgayKT.Text) && !string.IsNullOrWhiteSpace(txtNuDVC.Text) && !string.IsNullOrWhiteSpace(txtNamDVC.Text) && !string.IsNullOrWhiteSpace(txtNoidung.Text) && !string.IsNullOrWhiteSpace(txtChiPhi.Text) && !string.IsNullOrWhiteSpace(txtThu.Text))
                {
                    dataProcessor.ChangeData("UPDATE tblPhim SET tenPhim = '" + txtTenphim.Text + "',maQGSanXuat = '" + cboQuocgia.SelectedValue + "', maHangSX = '" + cboHangSX.SelectedValue + "', daoDien = '" + txtDaodien.Text + "',maTheLoai = '" + cboTheLoai.SelectedValue + "',ngayKhoiChieu = '" + txtNgayKC.Text + "', ngayKetThuc = '" + txtNgayKT.Text + "', nuDVC = '" + txtNuDVC.Text + "', namDVC = '" + txtNamDVC.Text + "', noiDungC = '" + txtNoidung.Text + "',tongChiPhi = '" + int.Parse(txtChiPhi.Text) + "', tongThu = '" + int.Parse(txtThu.Text) + "'WHERE maPhim = '" + txtID.Text + "'");

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
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
