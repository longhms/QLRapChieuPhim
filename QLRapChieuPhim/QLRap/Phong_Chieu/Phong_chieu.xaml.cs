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

namespace QLRapChieuPhim.QLRap
{
    /// <summary>
    /// Interaction logic for Phong_chieu.xaml
    /// </summary>
    public partial class Phong_chieu : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        const int numRows = 5;
        const int numSeatsPerRow = 6;
        string cS = "";
        public Phong_chieu()
        {
            InitializeComponent();
        }

        private void DrawSeats()
        {
            DataTable dt = dataProcessor.ReadData("SELECT * FROM tblChair");
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
                        seatIcon.Background = Brushes.LightGreen;
                    else if (chairStatus == "booked")
                        seatIcon.Background = Brushes.Red;

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = chairID;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;

                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(seatIcon);
                    

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
            DataTable dataTable = dataProcessor.ReadData("SELECT * FROM tblPhongChieu");
            cboPhongchieu.ItemsSource = dataTable.AsDataView();
            cboPhongchieu.DisplayMemberPath = "tenPhong";
            cboPhongchieu.SelectedValuePath = "maPhong";
        }

        private void cboPhongchieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cS = cboPhongchieu.SelectedValue.ToString() + "chairStatus";
            DrawSeats();
        }
    }
}
