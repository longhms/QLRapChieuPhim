using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QLRapChieuPhim.QLVe
{
    /// <summary>
    /// Interaction logic for Ban_ve.xaml
    /// </summary>
    public partial class Ban_ve : Window
    {
        Classes.DataProcessor dataProcessor = new Classes.DataProcessor(Login.cinemaID);
        public Ban_ve()
        {
            InitializeComponent();
        }
        void LoadData()
        {

        }
      

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboMaGG.Items.Add("Mã giảm giá 5%");
            cboMaGG.Items.Add("Mã giảm giá 10%");
            cboMaGG.Items.Add("Mã giảm giá 15%");
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

       
    }
}
