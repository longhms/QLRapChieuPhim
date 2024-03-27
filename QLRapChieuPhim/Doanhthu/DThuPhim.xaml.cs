using MaterialDesignThemes.Wpf;
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
using OfficeOpenXml;
using System.IO;
using System.Windows.Controls;
using System.Data.Entity;
using QLRapChieuPhim.Classes;
using Excel = Microsoft.Office.Interop.Excel;
namespace QLRapChieuPhim.Doanhthu
{
    /// <summary>
    /// Interaction logic for DThuPhim.xaml
    /// </summary>
    public partial class DThuPhim : Window
    {
        Classes.DataProcessor dtBase = new DataProcessor(Login.cinemaID);
        public DThuPhim()
        {
            InitializeComponent();
        }


        private void ExportToExcel(DataGrid dataGrid, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Thiết lập loại giấy phép EPPlus
            ExcelPackage excelPackage = new ExcelPackage();

            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            // Copy header
            for (int i = 1; i <= dataGrid.Columns.Count; i++)
            {
                worksheet.Cells[1, i].Value = dataGrid.Columns[i - 1].Header;
            }

            // Copy data
            for (int row = 0; row < dataGrid.Items.Count; row++)
            {
                if (dataGrid.Items[row] is DataRowView dataRowView)
                {
                    for (int col = 0; col < dataGrid.Columns.Count; col++)
                    {
                        var value = dataRowView[col];
                        worksheet.Cells[row + 2, col + 1].Value = value;
                    }
                }
            }

            // Save to file
            FileInfo excelFile = new FileInfo(filePath);
            excelPackage.SaveAs(excelFile);
        }



        private void btnIn_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                if (dtgDTPhim != null)
                {
                    ExportToExcel(dtgDTPhim, saveFileDialog.FileName);
                }
            }
        }
        void Header()
        {
            if (dtgDTPhim != null && dtgDTPhim.Columns != null)
            {
                dtgDTPhim.Columns[0].Header = "Mã Phim";
                dtgDTPhim.Columns[1].Header = "Tên Phim";
                dtgDTPhim.Columns[2].Header = "Thể loại";
                dtgDTPhim.Columns[3].Header = "Tổng chi phí";
                dtgDTPhim.Columns[4].Header = "Tổng thu";
                
            }
        }

        private void dtgDTPhim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sql = @"SELECT 
                    P.maPhim, 
                    P.tenPhim,  
                    T.tenTheLoai AS TenTheLoai, 
                    P.tongChiPhi,
                    P.tongThu 
                FROM tblPhim AS P
                LEFT JOIN tblTheLoai AS T ON P.maTheLoai = T.maTheLoai
                LEFT JOIN tblHangSX AS H ON P.maHangSX = H.maHangSX"
            ;

            DataTable dtPhim = dtBase.ReadData(sql);
            dtgDTPhim.ItemsSource = dtPhim.AsDataView();
            Header();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            LoadData();
        }
        void LoadData()
        {

            string sql = @"SELECT 
                    P.maPhim, 
                    P.tenPhim,  
                    T.tenTheLoai AS TenTheLoai, 
                    P.tongChiPhi,
                    P.tongThu 
                FROM tblPhim AS P
                LEFT JOIN tblTheLoai AS T ON P.maTheLoai = T.maTheLoai
                LEFT JOIN tblHangSX AS H ON P.maHangSX = H.maHangSX"
           ;
            DataTable dtPhim = dtBase.ReadData(sql);
            dtgDTPhim.ItemsSource = dtPhim.AsDataView();
            Header();
            CalculateTotalRevenue(dtPhim);
        }
        private void CalculateTotalRevenue(DataTable dataTable)
        {
            int totalRevenue = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["tongThu"] != DBNull.Value)
                {
                    totalRevenue += Convert.ToInt32(row["tongThu"]);
                }
            }
            txtTongThu.IsReadOnly = true;
            txtTongThu.Text = totalRevenue.ToString();
        }

    }
}

