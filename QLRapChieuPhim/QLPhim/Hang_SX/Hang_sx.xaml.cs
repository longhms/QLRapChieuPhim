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

namespace QLRapChieuPhim.QLPhim.Hang_SX
{
    /// <summary>
    /// Interaction logic for Hang_sx.xaml
    /// </summary>
    public partial class Hang_sx : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        public Hang_sx()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = dataProcessor.ReadData("SELECT * FROM tblHangSX");
                dgHang.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtHang = new DataTable();
            if (txtID.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã quốc gia");
                txtID.Focus();
                return;
            }
            dtHang = dataProcessor.ReadData("Select maHangSX, tenHangSX from tblHangSX WHERE maHangSX = ('" + txtID.Text + "') ");
            if (dtHang.Rows.Count > 0)
            {
                MessageBox.Show("Mã hãng sản xuất bị trùng lặp!");
                txtID.Focus();
                return;
            }
            dataProcessor.ChangeData("Insert into tblHangSX values('" + txtID.Text + "','" + txtTenHang.Text + "')");
            MessageBox.Show("Bạn đã thêm thành công!");
            LoadData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgHang.SelectedItem != null)
            {
                if (!string.IsNullOrWhiteSpace(txtID.Text) && !string.IsNullOrWhiteSpace(txtTenHang.Text))
                {

                    dataProcessor.ChangeData("UPDATE tblHangSX SET tenHangSX = '" + txtTenHang.Text + "' WHERE maHangSX = '" + txtID.Text + "'");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin hãng cần sửa.", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn hãng bạn muốn sửa thông tin!", "Thông báo");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa hãng này không ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {


                dataProcessor.ChangeData("Delete from tblHangSX WHERE maHangSX = ('" + txtID.Text + "')");
                LoadData();
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";
            if (txtID.Text.Trim() == "" && txtTenHang.Text.Trim() == "")
                sql = "Select maHangSX, tenHangSX from tblHangSX";
            else
            {
                sql = "Select maHangSX, tenHangSX from tblHangSX where maHangSX is not null ";
                if (txtID.Text.Trim() != "")
                    sql = sql + " and maHangSX like'%" + txtID.Text + "%'";
                if (txtTenHang.Text.Trim() != "")
                    sql = sql + " and tenHangSX like  '%" + txtTenHang.Text + "%'";
            }

            DataTable dtTimKiem = dataProcessor.ReadData(sql);
            dgHang.ItemsSource = dtTimKiem.AsDataView();
        }

        private void dgHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgHang.SelectedItem != null)
            {
                DataRowView selectedRow = dgHang.SelectedItem as DataRowView;
                txtID.IsEnabled = false;

                // Hiển thị thông tin từ dòng đã chọn lên 
                if (selectedRow != null)
                {
                    txtID.Text = selectedRow["maHangSX"].ToString();
                    txtTenHang.Text = selectedRow["tenHangSX"].ToString();
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
                txtTenHang.Text = "";
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
    }
}
