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
using COMExcel = Microsoft.Office.Interop.Excel;
using ExcelWindow = Microsoft.Office.Interop.Excel.Window;
using SystemWindow = System.Windows.Window;
using QLRapChieuPhim.QLRap.Lich_Chieu;
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
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

           
            for (int i = 1; i <= dataGrid.Columns.Count; i++)
            {
                worksheet.Cells[1, i].Value = dataGrid.Columns[i - 1].Header;
            }

            
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

            
            FileInfo excelFile = new FileInfo(filePath);
            excelPackage.SaveAs(excelFile);
        }

        private void btnIn_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                if (dtgDTPhim != null && dtgDTPhim.ItemsSource != null)
                {
                    COMExcel.Application exApp = new COMExcel.Application();
                    COMExcel.Workbook exBook;
                    COMExcel.Worksheet exSheet;
                    COMExcel.Range exRange;

                    exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
                    exSheet = exBook.Worksheets[1];

                    
                    exRange = exSheet.Cells[1, 1];
                    exRange.Font.Size = 10;
                    exRange.Font.Name = "Times New Roman";
                    exRange.Font.Bold = true;
                    exRange.Font.ColorIndex = 5;
                    exRange.ColumnWidth = 15;
                    exRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                    exRange.Value = "Tập đoàn tweltfthGROUP";
                    exRange = exSheet.Range["A1:B3"];
                    exRange.MergeCells = true;
                    exRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                    exRange.Value = "Hệ thống Rạp chiếu toàn quốc";
                    exRange = exSheet.Range["A3:B3"];
                    exRange.MergeCells = true;
                    exRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                    exRange.Value = "Hotline: +8441992809";
                    exRange = exSheet.Range["C2:H2"];
                    exRange.Font.Size = 16;
                    exRange.Font.Bold = true;
                    exRange.Font.ColorIndex = 3;
                    exRange.MergeCells = true;
                    exRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                    exRange.Value = "BÁO CÁO DOANH THU PHIM";

                    
                    ExportToExcel(dtgDTPhim, exSheet);
                    ExportToExcel(dtgDTPhim, exSheet);
                    
                    exBook.SaveAs(saveFileDialog.FileName);
                    exApp.Quit();
                }
            }
        }
        private void ExportToExcel(DataGrid dg, COMExcel.Worksheet exSheet)
        {
            
            int rowCount = dg.Items.Count;
            int columnCount = dg.Columns.Count;

            for (int j = 0; j < columnCount; j++)
            {
                exSheet.Cells[4, j + 1] = dg.Columns[j].Header;
            }

            
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var cellValue = ((TextBlock)dg.Columns[j].GetCellContent(dg.Items[i])).Text;
                    exSheet.Cells[i + 5, j + 1] = cellValue;
                }
            }
            
            int totalRevenue = 0;
            if (!string.IsNullOrEmpty(txtTongThu.Text))
            {
                totalRevenue = int.Parse(txtTongThu.Text);
            }
            
            exSheet.Cells[rowCount + 7, columnCount - 1] = "Tổng doanh thu:";
            exSheet.Cells[rowCount + 7, columnCount] = totalRevenue;
        }

        void Header()
        {
            if (dtgDTPhim != null && dtgDTPhim.Columns != null)
            {
                dtgDTPhim.Columns[0].Header = "Mã Phim";
                dtgDTPhim.Columns[1].Header = "Tên Phim";
                dtgDTPhim.Columns[2].Header = "Thể loại";
                dtgDTPhim.Columns[3].Header = "Ngày chiếu";
                dtgDTPhim.Columns[4].Header = "Tổng chi phí";
                dtgDTPhim.Columns[5].Header = "Lợi nhuận";
                
            }
        }

        private void dtgDTPhim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sql = @"SELECT 
            P.maPhim, 
            P.tenPhim,  
            T.tenTheLoai AS TenTheLoai, 
            BC.ngayChieu AS NgayKhoiChieu, -- Sử dụng ngày chiếu từ bảng BuoiChieu thay cho ngày khởi chiếu từ bảng Phim
            P.tongChiPhi,
            BC.tongTien 
            FROM tblPhim AS P
            LEFT JOIN tblTheLoai AS T ON P.maTheLoai = T.maTheLoai
            LEFT JOIN tblHangSX AS H ON P.maHangSX = H.maHangSX
            LEFT JOIN tblBuoiChieu AS BC ON P.maPhim = BC.maPhim";


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
            BC.ngayChieu AS NgayKhoiChieu, -- Sử dụng ngày chiếu từ bảng BuoiChieu thay cho ngày khởi chiếu từ bảng Phim
            P.tongChiPhi,
            BC.tongTien 
            FROM tblPhim AS P
            LEFT JOIN tblTheLoai AS T ON P.maTheLoai = T.maTheLoai
            LEFT JOIN tblHangSX AS H ON P.maHangSX = H.maHangSX
            LEFT JOIN tblBuoiChieu AS BC ON P.maPhim = BC.maPhim";
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
                if (row["tongTien"] != DBNull.Value)
                {
                    totalRevenue += Convert.ToInt32(row["tongTien"]);
                }
            }
            txtTongThu.IsReadOnly = true;
            txtTongThu.Text = totalRevenue.ToString();
        }

        private void CalculateTotalRevenueWithinDateRange(DataTable dataTable)
        {
            int totalRevenue = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["tongTien"] != DBNull.Value)
                {
                    totalRevenue += Convert.ToInt32(row["tongTien"]);
                }
            }
            txtTongThu.IsReadOnly = true;
            txtTongThu.Text = totalRevenue.ToString();
        }
        private void SearchByDateRange(DateTime fromDate, DateTime toDate)
        {
            
            string fromDateStr = fromDate.ToString("yyyy/MM/dd");
            string toDateStr = toDate.ToString("yyyy/MM/dd");

            
            string sql = @"
            SELECT 
            P.maPhim, 
            P.tenPhim,  
            T.tenTheLoai AS TenTheLoai, 
            BC.ngayChieu AS NgayChieu,
            P.tongChiPhi,
            BC.tongTien 
            FROM tblPhim AS P
            LEFT JOIN tblBuoiChieu AS BC ON P.maPhim = BC.maPhim
            LEFT JOIN tblTheLoai AS T ON P.maTheLoai = T.maTheLoai
            WHERE BC.ngayChieu BETWEEN '" + fromDateStr + "' AND '" + toDateStr + "'";

            
            DataTable dataTable = dtBase.ReadData(sql);
            dtgDTPhim.ItemsSource = dataTable.AsDataView();
            CalculateTotalRevenueWithinDateRange(dataTable);
        }

        private void dtpNC_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime fromDate = dtpNC.SelectedDate ?? DateTime.MinValue;
            DateTime toDate = dtpNC2.SelectedDate ?? DateTime.MaxValue;
            SearchByDateRange(fromDate, toDate);
            Header();
        }

        private void dtpNC2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime fromDate = dtpNC.SelectedDate ?? DateTime.MinValue;
            DateTime toDate = dtpNC2.SelectedDate ?? DateTime.MaxValue;
            SearchByDateRange(fromDate, toDate);
            Header();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}

