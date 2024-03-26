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

namespace QLRapChieuPhim.QLPhim.The_Loai
{
    /// <summary>
    /// Interaction logic for The_loai.xaml
    /// </summary>
    public partial class The_loai : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        public The_loai()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            try
            {
                DataTable dt = dataProcessor.ReadData("SELECT * FROM tblTheLoai");
                dgTheLoai.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }





        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát Không?", "Thong bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtTheLoai = new DataTable();
            if (txtID.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã thể loại");
                txtID.Focus();
                return;
            }
            dtTheLoai = dataProcessor.ReadData("Select maTheLoai, tenTheloai from tblTheLoai WHERE maTheLoai = ('" + txtID.Text + "') ");
            if (dtTheLoai.Rows.Count > 0)
            {
                MessageBox.Show("Mã thể loại bị trùng lặp!");
                txtID.Focus();
                return;
            }
            dataProcessor.ChangeData("Insert into tblTheLoai values('" + txtID.Text + "','" + txtTenTheLoai.Text + "')");
            MessageBox.Show("Bạn đã thêm thành công!");
            LoadData();
        }

        private void dgTheLoai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTheLoai.SelectedItem != null)
            {
                DataRowView selectedRow = dgTheLoai.SelectedItem as DataRowView;
                txtID.IsEnabled = false;

                // Hiển thị thông tin từ dòng đã chọn lên 
                if (selectedRow != null)
                {
                    txtID.Text = selectedRow["maTheLoai"].ToString();
                    txtTenTheLoai.Text = selectedRow["tenTheLoai"].ToString();
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
                txtTenTheLoai.Text = "";
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            if (dgTheLoai.SelectedItem != null)
            {
                if (!string.IsNullOrWhiteSpace(txtID.Text) && !string.IsNullOrWhiteSpace(txtTenTheLoai.Text))
                {
                    
                    dataProcessor.ChangeData("UPDATE tblTheLoai SET tenTheLoai = '" + txtTenTheLoai.Text + "' WHERE maTheLoai = '" + txtID.Text + "'");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin thể loại phim cần sửa.", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn thể loại phim bạn muốn sửa thông tin!", "Thông báo");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa thể loại phim này không ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {


                dataProcessor.ChangeData("Delete from tblTheLoai WHERE maTheLoai = ('" + txtID.Text + "')");
                LoadData();
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";
            if (txtID.Text.Trim() == "" && txtTenTheLoai.Text.Trim() == "")
                sql = "Select maTheLoai, tenTheLoai from tblTheLoai";
            else
            {
                sql = "Select maTheLoai, tenTheLoai from tblTheLoai where maTheLoai is not null ";
                if (txtID.Text.Trim() != "")
                    sql = sql + " and maTheLoai like'%" + txtID.Text + "%'";
                if (txtTenTheLoai.Text.Trim() != "")
                    sql = sql + " and tenTheLoai like  '%" + txtTenTheLoai.Text + "%'";
            }

            DataTable dtTimKiem = dataProcessor.ReadData(sql);
            dgTheLoai.ItemsSource = dtTimKiem.AsDataView();
           
        }
    }   
}
