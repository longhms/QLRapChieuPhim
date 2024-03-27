using MaterialDesignThemes.Wpf;
using QLRapChieuPhim.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static MaterialDesignThemes.Wpf.Theme;

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

        private ICollectionView dataView;
        public Lich_chieu()
        {
            InitializeComponent();
            dataView = CollectionViewSource.GetDefaultView(dgBuoiChieu.ItemsSource);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable data = dataProcessor.ReadData("SELECT * FROM tblPhongChieu");
            cboPhong.ItemsSource = data.AsDataView();
            cboPhong.DisplayMemberPath = "tenPhong";
            cboPhong.SelectedValuePath = "maPhong";
            LoadData(sql);

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
            string phongChieu = cboPhong.SelectedValue?.ToString();

            DateTime ngayChieu = dtpNgayChieu.SelectedDate ?? DateTime.MinValue;

            string ngayChieuStr = ngayChieu.ToString("yyyy/MM/dd");

            string sqlNC = sql + " AND ngayChieu LIKE '%" + ngayChieuStr + "%'";

            if(phongChieu != null)
            {
                sqlNC = sqlNC + " AND maPhong LIKE '%" + phongChieu + "%'";
            }

            /*DataTable dataTable = dataProcessor.ReadData(sqlNC);
            dgBuoiChieu.ItemsSource = dataTable.AsDataView();

            cboPhong.SelectedItem = null;

            dataTable = dataProcessor.ReadData(sql);*/

            LoadData(sqlNC);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cboPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            string phongChieu = cboPhong.SelectedValue?.ToString();
            DateTime ngayChieu = dtpNgayChieu.SelectedDate ?? DateTime.MinValue;

            string ngayChieuStr = ngayChieu.ToString("yyyy/MM/dd");

            string sqlP = sql + " AND maPhong LIKE '%" + phongChieu + "%'";

            if(ngayChieu != DateTime.MinValue)
            {
                sqlP = sqlP + " AND ngayChieu LIKE '%" + ngayChieuStr + "%'";
            }

            
            /*DataTable dataTable = dataProcessor.ReadData(sqlP);
            dgBuoiChieu.ItemsSource = dataTable.AsDataView();

            dtpNgayChieu.SelectedDate = null;

            dataTable = dataProcessor.ReadData(sql);*/

            LoadData(sqlP);
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

        private void btnRf_Click(object sender, RoutedEventArgs e)
        {
            /*Lich_chieu lich_Chieu = new Lich_chieu();
            this.Close();
            lich_Chieu.ShowDialog();*/

            LoadData();
        }

        void LoadData(string sql )
        {
            DataTable dataTable = dataProcessor.ReadData(sql);
            dgBuoiChieu.ItemsSource = dataTable.AsDataView();
            
        }

        void LoadData()
        {
            DataTable dataTable = dataProcessor.ReadData(sql);
            dgBuoiChieu.ItemsSource = dataTable.AsDataView();
            dtpNgayChieu.SelectedDate = null;
            cboPhong.SelectedItem = null;
            txtFind.Text = null;
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            string sql1 = $"SELECT maShow,tP.tenPhim,maPhong,ngayChieu,maGioChieu FROM tblBuoiChieu tB INNER JOIN tblPhim tP ON tP.maPhim = tB.maPhim";

            if (!string.IsNullOrEmpty(txtFind.Text.Trim()))
            {
                sql1 += " AND (tB.maShow LIKE '%" + txtFind.Text + "%' OR ";
                sql1 += "tP.tenPhim LIKE '%" + txtFind.Text + "%' OR ";
                sql1 += "tB.maPhong LIKE '%" + txtFind.Text + "%' OR ";
                sql1 += "tB.ngayChieu LIKE '%" + txtFind.Text + "%' OR ";
                sql1 += "tB.maGioChieu LIKE '%" + txtFind.Text + "%') ";
                
            }

            LoadData(sql1);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string test = dtpNgayChieu.SelectedDate?.ToString();

        }
    }
}
