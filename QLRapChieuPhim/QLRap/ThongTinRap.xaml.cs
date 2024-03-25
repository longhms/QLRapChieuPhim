using System;
using System.Collections.Generic;
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
        public ThongTinRap()
        {
            InitializeComponent();
        }

        public void readIntro()
        {
            string filePath = Login.cinemaID + "QLRap";

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
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Lỗi khi đọc tệp: " + ex.Message);
            }
        }
    }
}
