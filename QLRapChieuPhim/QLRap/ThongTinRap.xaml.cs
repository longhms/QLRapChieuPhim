using QLRapChieuPhim.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace QLRapChieuPhim.QLRap
{
    /// <summary>
    /// Interaction logic for ThongTinRap.xaml
    /// </summary>
    public partial class ThongTinRap : Window
    {
        DataProcessor dtBase = new DataProcessor(Login.cinemaID);
        public ThongTinRap()
        {
            InitializeComponent();
            Load();
            readIntro();
        }

        public void readIntro()
        {
            
            DataTable dt = dtBase.ReadData("SELECT * FROM tblRap");
            txtCinName.Text = dt.Rows[0]["tenRap"].ToString();

            string filePath = "D:/Long hoc bai/dotNet/QLRapChieuPhim/QLRapChieuPhim/QLRap/"+ Login.cinemaID +"QLRap.txt";

            try
            {
                
                if (File.Exists(filePath))
                {
                    
                    string content = File.ReadAllText(filePath);

                    
                    txtGioiThieu.Text = content;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tệp.");
                    MessageBox.Show(filePath);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Lỗi khi đọc tệp: " + ex.Message);
            }
        }

        private void FlipperClassic_IsFlippedChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {

        }

        private void btnAddAdmin_Click(object sender, RoutedEventArgs e)
        {
            Rap.Rap addAdmn = new Rap.Rap();
            addAdmn.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        void Load()
        {
            DataTable dt = dtBase.ReadData("SELECT * FROM tblRap");
            string sdt = dt.Rows[0]["dienThoai"].ToString();
            string dchi = dt.Rows[0]["diaChi"].ToString();
            btnSDT.Content = sdt;
            txtDiaChi.Text = dchi;
        }
    }
}
