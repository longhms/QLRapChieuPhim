using QLRapChieuPhim.Classes;
using QLRapChieuPhim.TimKiem;
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
        private int tongThu;

        public Them_phim()
        {
            InitializeComponent();
            txtThu.Text = tongThu.ToString();
            txtThu.IsReadOnly = true;
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
            dataProcessor.ChangeData("insert into tblPhim values('" + txtID.Text + "','" + txtTenphim.Text + "','" + cboQuocgia.SelectedValue.ToString()+ "','" + cboHangSX.SelectedValue.ToString() + "','" + txtDaodien.Text + "','" + cboTheLoai.SelectedValue.ToString() + "','" + txtNgayKC.Text + "','" + txtNgayKT.Text + "','" + txtNuDVC.Text + "','" + txtNamDVC.Text + "','" + txtNoidung.Text + "','" + txtChiPhi.Text + "','" + txtThu.Text + "') ");
            MessageBox.Show("Thêm thành công");

            try
            {
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

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
