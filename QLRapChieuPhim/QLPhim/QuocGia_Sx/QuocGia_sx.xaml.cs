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

namespace QLRapChieuPhim.QLPhim.QuocGia_Sx
{
    /// <summary>
    /// Interaction logic for QuocGia_sx.xaml
    /// </summary>
    public partial class QuocGia_sx : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        public QuocGia_sx()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            try
            {
                DataTable dt = dataProcessor.ReadData("SELECT * FROM tblQGsanXuat");
                dgQuocGia.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtQuocGia = new DataTable();
            if (txtID.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã quốc gia");
                txtID.Focus();
                return;
            }
            dtQuocGia = dataProcessor.ReadData("Select maQGSanXuat, tenQGSanXuat from tblQGsanXuat WHERE maQGSanXuat = ('" + txtID.Text + "') ");
            if (dtQuocGia.Rows.Count > 0)
            {
                MessageBox.Show("Mã quốc gia bị trùng lặp!");
                txtID.Focus();
                return;
            }
            dataProcessor.ChangeData("Insert into tblQGsanXuat values('" + txtID.Text + "','" + txtTenQuocGia.Text + "')");
            MessageBox.Show("Bạn đã thêm thành công!");
            LoadData();
        }

        private void dgQuocGia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgQuocGia.SelectedItem != null)
            {
                DataRowView selectedRow = dgQuocGia.SelectedItem as DataRowView;
                txtID.IsEnabled = false;

                // Hiển thị thông tin từ dòng đã chọn lên 
                if (selectedRow != null)
                {
                    txtID.Text = selectedRow["maQGSanXuat"].ToString();
                    txtTenQuocGia.Text = selectedRow["tenQGSanXuat"].ToString();
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
                txtTenQuocGia.Text = "";
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            if (dgQuocGia.SelectedItem != null)
            {
                if (!string.IsNullOrWhiteSpace(txtID.Text) && !string.IsNullOrWhiteSpace(txtTenQuocGia.Text))
                {

                    dataProcessor.ChangeData("UPDATE tblQGsanXuat SET tenQGSanXuat = '" + txtTenQuocGia.Text + "' WHERE maQGSanXuat = '" + txtID.Text + "'");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin quốc gia cần sửa.", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn quóc gia bạn muốn sửa thông tin!", "Thông báo");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa quốc gia này không ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {


                dataProcessor.ChangeData("Delete from tblQGsanXuat WHERE maQGSanXuat = ('" + txtID.Text + "')");
                LoadData();
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";
            if (txtID.Text.Trim() == "" && txtTenQuocGia.Text.Trim() == "")
                sql = "Select maQGSanXuat, tenQGSanXuat from tblQGsanXuat";
            else
            {
                sql = "Select maQGSanXuat, tenQGSanXuat from tblQGsanXuat where maQGSanXuat is not null ";
                if (txtID.Text.Trim() != "")
                    sql = sql + " and maQGSanXuat like'%" + txtID.Text + "%'";
                if (txtTenQuocGia.Text.Trim() != "")
                    sql = sql + " and tenQGSanXuat like  '%" + txtTenQuocGia.Text + "%'";
            }

            DataTable dtTimKiem = dataProcessor.ReadData(sql);
            dgQuocGia.ItemsSource = dtTimKiem.AsDataView();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát ứng dụng?", "Thong bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
