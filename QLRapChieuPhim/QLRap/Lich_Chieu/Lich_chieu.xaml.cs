using MaterialDesignThemes.Wpf;
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

namespace QLRapChieuPhim.QLRap.Lich_Chieu
{
    /// <summary>
    /// Interaction logic for Lich_chieu.xaml
    /// </summary>
    public partial class Lich_chieu : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        string sql = $"SELECT maShow,tP.tenPhim,maPhong,ngayChieu,maGioChieu FROM tblBuoiChieu tB INNER JOIN tblPhim tP ON tP.maPhim = tB.maPhim";
        string testMS;
        public Lich_chieu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable data = dataProcessor.ReadData("SELECT * FROM tblPhongChieu");
            cboPhong.ItemsSource = data.AsDataView();
            cboPhong.DisplayMemberPath = "tenPhong";
            cboPhong.SelectedValuePath = "maPhong";

            DataTable dataTable = dataProcessor.ReadData(sql);
            dgBuoiChieu.ItemsSource = dataTable.AsDataView();

        }

        /*void LoadData()
        {
            try
            {
                DataTable dt = dataProcessor.ReadData("SELECT * FROM tblPhim");
                dgBuoiChieu.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }*/

        private void dtpNgayChieu_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
            DateTime ngayChieu = dtpNgayChieu.SelectedDate ?? DateTime.MinValue;

            string ngayChieuStr = ngayChieu.ToString("yyyy/MM/dd");

            string sqlNC = sql + " AND ngayChieu LIKE '%" + ngayChieuStr + "%'";

            DataTable dataTable = dataProcessor.ReadData(sqlNC);
            dgBuoiChieu.ItemsSource = dataTable.AsDataView();

            cboPhong.SelectedItem = null;

            dataTable = dataProcessor.ReadData(sql);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cboPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            string phongChieu = cboPhong.SelectedValue?.ToString();
            string sqlP = sql + " AND maPhong LIKE '%" + phongChieu + "%'";

            DataTable dataTable = dataProcessor.ReadData(sqlP);
            dgBuoiChieu.ItemsSource = dataTable.AsDataView();

            dtpNgayChieu.SelectedDate = null;

            dataTable = dataProcessor.ReadData(sql);
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            ThemBuoiChieu themBuoiChieu = new ThemBuoiChieu();
            themBuoiChieu.ShowDialog();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {

            DataRowView selectedRow = dgBuoiChieu.SelectedItem as DataRowView;

            testMS = selectedRow["maShow"].ToString();

            TTBuoiChieu tTBuoiChieu = new TTBuoiChieu(testMS);
            tTBuoiChieu.ShowDialog();
        }
    }
}
