using QLRapChieuPhim.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

namespace QLRapChieuPhim.QLRap
{
    /// <summary>
    /// Interaction logic for Phong_chieu.xaml
    /// </summary>
    public partial class Phong_chieu : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        
        
        int numRows;
        int numSeatsPerRow = 6;
        string cS = "";
        public Phong_chieu()
        {
            InitializeComponent();
        }

        private void DrawSeats()
        {
            DataTable dt = dataProcessor.ReadData("SELECT * FROM tblChair");
            int recordCount = dataProcessor.CountRecords("tblChair");
            numRows = recordCount / 6;
            int x = 65;
            char y = (char)x;

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
                    string chairStatus = row[cS].ToString();
                    string chairID = row["ID"].ToString();

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Vertical;

                    

                    MaterialDesignThemes.Wpf.PackIcon seatIcon = new MaterialDesignThemes.Wpf.PackIcon();
                    seatIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Seat;
                    seatIcon.Width = 50;
                    seatIcon.Height = 50;
                    seatIcon.Margin = new Thickness(5);
                    /*seatIcon.Content = chairID;*/
                    seatIcon.Tag = chairID; // Set Tag to chair ID for later reference

                    // Set color based on chairStatus
                    if (chairStatus == "empty")
                        seatIcon.Background = Brushes.LawnGreen;
                    else if (chairStatus == "booked")
                        seatIcon.Background = Brushes.OrangeRed;

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = chairID;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;

                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(seatIcon);
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.VerticalAlignment = VerticalAlignment.Center;

                    // Add button to grid
                    Grid.SetColumn(stackPanel, j);
                    Grid.SetRow(stackPanel, i);
                    grid.Children.Add(stackPanel);
                }
            }


            // Add the grid to the main window
            mainGrid.Children.Add(grid);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void cboPhongchieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRefreshRoom.IsEnabled = true;
            btnDeleteRoom.IsEnabled = true;
            cS = cboPhongchieu.SelectedValue.ToString() + "chairStatus";
            DrawSeats();
        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if(MessageBox.Show("Bạn có chắc chắn muốn thêm phòng chiếu phim?","Thông báo",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DataTable data = dataProcessor.ReadData("SELECT maPhong FROM tblPhongChieu");
                    string maRap = "R" + Login.cinemaID;
                    string maPhong = data.Rows[data.Rows.Count - 1]["maPhong"].ToString();
                    int maPhongInt = int.Parse(maPhong) + 1;

                    string tenPhong = "Phòng " + maPhongInt;
                    int soGhe = 30;

                    string column = maPhongInt + "chairStatus";
                    string addColumn = $"ALTER TABLE tblChair ADD COLUMN [{column}] TEXT";
                    string statusColumn = $"UPDATE tblChair SET [{column}] = 'empty'";

                    dataProcessor.ChangeData($"INSERT into tblPhongChieu values('" + maRap + "','" + maPhongInt + "','" + tenPhong + "','" + soGhe + "')");
                    dataProcessor.ChangeData(addColumn);
                    dataProcessor.ChangeData(statusColumn);
                    /*dataProcessor.ChangeData($"UPDATE tblRap SET soPhong = soPhong + {1}");
                    dataProcessor.ChangeData($"UPDATE tblRap SET tongSoGhe = tongSoGhe + {30}");*/

                    this.Close();
                    Phong_chieu phong_Chieu = new Phong_chieu();
                    phong_Chieu.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        void LoadData()
        {
            DataTable data = dataProcessor.ReadData("SELECT * FROM tblPhongChieu");
            cboPhongchieu.ItemsSource = data.AsDataView();
            cboPhongchieu.DisplayMemberPath = "tenPhong";
            cboPhongchieu.SelectedValuePath = "maPhong";
        }

        private void btnRf_Click(object sender, RoutedEventArgs e)
        {
            Phong_chieu phong_Chieu = new Phong_chieu();
            this.Close();
            phong_Chieu.ShowDialog();
        }

        private void btnRefreshRoom_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn làm mới phòng?","Thông báo",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string statusColumn = $"UPDATE tblChair SET [{cS}] = 'empty'";
                dataProcessor.ChangeData(statusColumn);
                DrawSeats();
            }
        }

        private void btnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn xóa "+cS+" hay không? ", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                
                dataProcessor.ChangeData("Delete from tblPhongChieu WHERE maPhong = ('" + cboPhongchieu.SelectedValue + "')");
                dataProcessor.ChangeData($"ALTER TABLE tblChair DROP COLUMN [{cS}]");
                dataProcessor.ChangeData($"UPDATE tblRap SET soPhong = soPhong - {1}");
                dataProcessor.ChangeData($"UPDATE tblRap SET tongSoGhe = tongSoGhe - {30}");

                this.Close();
                Phong_chieu phong_Chieu = new Phong_chieu();
                phong_Chieu.ShowDialog();

            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
