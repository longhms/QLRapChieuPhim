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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLRapChieuPhim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        Classes.DataProcessor dtBase = new DataProcessor(cinemaID);
        Classes.Common cm = new Classes.Common();
        public static string userName = "", mk;
        public static string cinemaID = "";

        Classes.CinemaHall[] cinemaHalls = new CinemaHall[3];
        
        

        public Login()
        {
            InitializeComponent();
            cinemaHalls[0] = new CinemaHall("1", "KMC Thái Bình");
            cinemaHalls[2] = new CinemaHall("3", "KMC Vĩnh Phúc");
            cinemaHalls[1] = new CinemaHall("2", "KMC Lào Cai");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*DataTable dtRap = dtBase.ReadData("Select maRap,tenRap from tblRap");
            cboRapCP.ItemsSource = dtRap.AsDataView();
            *//*cboRapCP.Items.Add("Chọn Rạp");*//*
            cboRapCP.DisplayMemberPath = "tenRap";
            cboRapCP.SelectedValuePath = "maRap";*/

            
            cboRapCP.DisplayMemberPath = "cinemaHallName";
            cboRapCP.SelectedValuePath = "cinemaHallID";
            cboRapCP.ItemsSource = cinemaHalls;

            /*cinemaID = cboRapCP.SelectedValue.ToString();*/

        }

        private void btnDangnhap_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";

            

            if (txtName.Text.Trim() == "" || psbPassword.Password == "" || cboRapCP.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập tài khoản và mật khẩu!");
                return;
            }
            /*mk = cm.CreatePassword(psbPassword.Password);*/
            mk = psbPassword.Password;
            userName = txtName.Text;
            cinemaID = cboRapCP.SelectedValue.ToString();

            Classes.DataProcessor dtBase = new DataProcessor(cinemaID);

            sql = "Select * from tblRap where userName = '" + txtName.Text + "' and password = '" + mk + "'";
            DataTable dtTaiKhoan = dtBase.ReadData(sql);
            if (dtTaiKhoan.Rows.Count == 0)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!");
                return;
            }
            HomePage homePage = new HomePage();
            this.Hide();
            homePage.ShowDialog();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát ứng dụng?", "Thong bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }
    }
}
