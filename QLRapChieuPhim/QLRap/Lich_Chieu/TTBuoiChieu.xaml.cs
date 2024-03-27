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
    /// Interaction logic for TTBuoiChieu.xaml
    /// </summary>
    public partial class TTBuoiChieu : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        int numRows = 5;
        int numSeatsPerRow = 6;
        string testMS1;
        string sql ;
        public TTBuoiChieu()
        {
            InitializeComponent();
        }

        public TTBuoiChieu(string testMS)
        {
            InitializeComponent();
            testMS1 = testMS;
            DrawSeats();
            LoadData();
        }

        private void DrawSeats()
        {
            DataTable dt = dataProcessor.ReadData("SELECT * FROM tblChair");
            
            DataTable data = dataProcessor.ReadData("SELECT hangGhe,soGhe FROM tblVe WHERE maShow = ('"+ testMS1 +"')");
            string[] test = new string[data.Rows.Count];

            for (int i = 0; i < data.Rows.Count; i++)
            {
                test[i] = data.Rows[i]["hangGhe"].ToString() + data.Rows[i]["soGhe"].ToString();
            }
            

            if (dt.Rows.Count != numRows * numSeatsPerRow)
            {
                MessageBox.Show("Data from database doesn't match the expected number of seats.");
                return;
            }

            // Create a Grid to hold the seats
            Grid grid = new Grid();
            for (int i = 0; i < numRows; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                grid.RowDefinitions.Add(rowDef);
            }

            for (int i = 0; i < numSeatsPerRow; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                grid.ColumnDefinitions.Add(colDef);
            }

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numSeatsPerRow; j++)
                {

                    DataRow row = dt.Rows[i * numSeatsPerRow + j];
                    
                    string chairID = row["ID"].ToString();

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Vertical;



                    MaterialDesignThemes.Wpf.PackIcon seatIcon = new MaterialDesignThemes.Wpf.PackIcon();
                    seatIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Seat;
                    seatIcon.Width = 30;
                    seatIcon.Height = 30;
                    seatIcon.Margin = new Thickness(5);
                    /*seatIcon.Content = chairID;*/
                    seatIcon.Tag = chairID; 

                    

                    if(test.Length > 0)
                    {
                        for (int k = 0; k < test.Length; k++)
                        {
                            if (chairID == test[k].ToString())
                            {
                                seatIcon.Background = Brushes.OrangeRed;
                                break;
                            }
                            else
                            {
                                seatIcon.Background = Brushes.LawnGreen;
                            }
                        }

                    }
                    else
                    {
                        seatIcon.Background = Brushes.LawnGreen;
                    }


                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = chairID;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;

                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(seatIcon);
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.VerticalAlignment = VerticalAlignment.Center;

                    
                    Grid.SetColumn(stackPanel, j);
                    Grid.SetRow(stackPanel, i);
                    grid.Children.Add(stackPanel);
                }
            }


            
            mainGrid.Children.Add(grid);
        }

        void LoadData()
        {
            sql = $"SELECT tP.tenPhim,tPC.tenPhong,ngayChieu,maGioChieu FROM tblBuoiChieu tB INNER JOIN tblPhim tP ON tP.maPhim = tB.maPhim INNER JOIN tblPhongChieu tPC ON tPC.maPhong = tB.maPhong WHERE maShow =('{testMS1}')";
            DataTable data = dataProcessor.ReadData(sql);
            lblMovieName.Content = data.Rows[0]["tenPhim"].ToString();
            lblNgayChieu.Content = data.Rows[0]["ngayChieu"].ToString();
            lblPhongChieu.Content = data.Rows[0]["tenPhong"].ToString();
            lblXuatChieu.Content = data.Rows[0]["maGioChieu"].ToString();

            string testTime = data.Rows[0]["maGioChieu"].ToString();
            DateTime testDate = DateTime.Parse(testTime);
            int testInt = testDate.Hour;
            string testStr;

            if(testInt < 10)
            {
                testStr = "0" + testInt.ToString() + ":00";
            } else
            {
                testStr = testInt.ToString() + ":00";
            }

            data = dataProcessor.ReadData($"SELECT * FROM tblGioChieu WHERE maGioChieu = ('{testStr}')");
            lblGiaVe.Content = data.Rows[0]["donGia"].ToString();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDeleteLC_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bạn không thể xóa lịch chiếu đã thêm!","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
            return;
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
