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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QLRapChieuPhim.QLRap.Lich_Chieu
{
    /// <summary>
    /// Interaction logic for ThemBuoiChieu.xaml
    /// </summary>
    public partial class ThemBuoiChieu : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        public ThemBuoiChieu()
        {
            InitializeComponent();
            LoadData();
        }

        private void btnThemBC_Click(object sender, RoutedEventArgs e)
        {
            

            DateTime ngayChieu = dtpNgayCh.SelectedDate ?? DateTime.MinValue;

            string ngayChieuStr = ngayChieu.ToString("yyyy/MM/dd");

            DateTime giochieu = tmpGioChieu.SelectedTime ?? DateTime.MinValue;
            int test = giochieu.Hour;
            string gioChieuStr = giochieu.ToString("HH:mm");

            string maPhong = cboPhongChieu.SelectedValue?.ToString();

            DataTable dt = dataProcessor.ReadData("SELECT * FROM tblBuoiChieu WHERE ngayChieu = ('" + ngayChieuStr + "') AND maPhong LIKE '%"+ maPhong +"%'");

            string phim = cboMovieName.SelectedValue?.ToString();

            string maRap = "R" +Login.cinemaID;


            int tmpp = 0;

            if(dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tmp = dt.Rows[i]["maGioChieu"].ToString();
                    DateTime tmpDT = DateTime.Parse(tmp);
                    int tmpI = tmpDT.Hour;
                    if (test - tmpI < 3)
                    {
                        tmpp++;
                    }
                }
            }

            if(test > 22 || test < 8)
            {
                MessageBox.Show("Hãy nhập giờ tương ứng với giờ hoạt động của rạp!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cboMovieName.Text == "" || ngayChieuStr == null || gioChieuStr == null || cboPhongChieu.Text == "" || txtMaBuoiChieu.Text =="")
            {
                MessageBox.Show("Hãy nhập đủ thông tin để thêm!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn thêm Buổi chiếu ?","Thông báo",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (tmpp > 0)
                {
                    MessageBox.Show("Có vẻ đã có một buổi chiếu trùng hoặc chưa thể kết thúc vào lúc đó!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataProcessor.ChangeData($"INSERT INTO tblBuoiChieu values('{txtMaBuoiChieu.Text}','{phim}','{maRap}','{maPhong}','{ngayChieuStr}','{gioChieuStr}','{0}','{0}')");
            }
        }

        void LoadData()
        {
            DataTable dt = dataProcessor.ReadData("SELECT * FROM tblPhongChieu");
            cboPhongChieu.ItemsSource = dt.AsDataView();
            cboPhongChieu.DisplayMemberPath = "tenPhong";
            cboPhongChieu.SelectedValuePath = "maPhong";

            DataTable dt2 = dataProcessor.ReadData("SELECT * FROM tblPhim");
            cboMovieName.ItemsSource = dt2.AsDataView();
            cboMovieName.DisplayMemberPath = "tenPhim";
            cboMovieName.SelectedValuePath = "maPhim";
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime ngayChieu = dtpNgayCh.SelectedDate ?? DateTime.MinValue;

            string ngayChieuStr = ngayChieu.ToString("yyyy/MM/dd");
        }

        private void tmpGioChieu_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            int gio = tmpGioChieu.SelectedTime?.Hour ?? 0;
            string gioStr;
            if(gio < 10)
            {
                gioStr = "0" + gio + ":00" ;
            }
            else
            {
                gioStr = gio.ToString() + ":00";
            }

            DataTable dt = dataProcessor.ReadData("SELECT * FROM tblGioChieu");
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                if(gioStr == dt.Rows[i]["maGioChieu"].ToString())
                {
                    txtGiaVe.Text = dt.Rows[i]["donGia"].ToString();
                    return;
                }
            }
        }
    }
}
