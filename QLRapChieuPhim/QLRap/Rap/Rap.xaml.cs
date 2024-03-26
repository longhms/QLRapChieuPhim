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

namespace QLRapChieuPhim.QLRap.Rap
{
    /// <summary>
    /// Interaction logic for Rap.xaml
    /// </summary>
    public partial class Rap : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        public Rap()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddAD_Click(object sender, RoutedEventArgs e)
        {
            if(txtUsername.Text == null)
            {
                MessageBox.Show("Hãy nhập Username!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                txtUsername.Focus();
                return;
            } else if(psbPass.Password == null)
            {
                MessageBox.Show("Hãy nhập Password!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                psbPass.Focus();
                return;
            }
            if(MessageBox.Show("Bạn có chắc muốn thêm tài khoản Admin?","Thông báo",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DataTable dt = new DataTable();
                dt = dataProcessor.ReadData("SELECT * FROM tblRap WHERE Username = ('" + txtUsername.Text + "')");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Đã có Username trùng!", "Lỗi",MessageBoxButton.OK, MessageBoxImage.Error);
                    txtUsername.Focus();
                    return;
                }
                DataTable data = dataProcessor.ReadData("SELECT * FROM tblRap");
                string maRap = data.Rows[0]["maRap"].ToString();
                string tenRap = data.Rows[0]["tenRap"].ToString();
                string diaChi = data.Rows[0]["diaChi"].ToString();

                dataProcessor.ChangeData($"INSERT into tblRap values('" + maRap + "','" + tenRap + "','" + diaChi + "','" + null + "','" + null + "','" + null + "','" + txtUsername.Text + "','" + psbPass.Password + "')");
            }
        }

        private void btnDeleteAD_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Hãy nhập Username cần xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                txtUsername.Focus();
                return;
            }
            else if (psbPass.Password == "")
            {
                MessageBox.Show("Hãy nhập Password để xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                psbPass.Focus();
                return;
            } else if(txtUsername.Text == Login.userName)
            {
                MessageBox.Show("Bạn không thể xóa tài khoản đang đăng nhập", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                
                return;
            }

            if(MessageBox.Show("Bạn có chắc muốn xóa tài khoản Admin?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DataTable dt = new DataTable();
                dt = dataProcessor.ReadData("SELECT * FROM tblRap WHERE Username = ('" + txtUsername.Text + "')");
                if(dt.Rows.Count > 0)
                {
                    string check = dt.Rows[0]["password"].ToString();

                    if (psbPass.Password == check)
                    {
                        dataProcessor.ChangeData($"Delete from tblRap WHERE userName = ('" + txtUsername.Text + "')");
                        MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else if (psbPass.Password != check)
                    {
                        MessageBox.Show("Bạn phải nhập đúng tài khoản và mật khẩu để xóa tài khoản Admin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }

                MessageBox.Show("Không tìm thấy tài khoản "+txtUsername.Text+"!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
        }
    }
}
