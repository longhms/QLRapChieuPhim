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
        Classes.DataProcessor dataProcessor = new DataProcessor();
        const int numRows = 5;
        const int numSeatsPerRow = 6;
        string cS = Login.cinemaID + "chairStatus";
        public Phong_chieu()
        {
            InitializeComponent();
            DrawSeats();
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
                    Button seatButton = new Button();
                    DataRow row = dt.Rows[i * numSeatsPerRow + j];
                    string chairStatus = row[cS].ToString();
                    string chairID = row["ID"].ToString();

                    // Set button properties
                    seatButton.Width = 50;
                    seatButton.Height = 50;
                    seatButton.Margin = new Thickness(5);
                    seatButton.Content = chairID;
                    seatButton.Tag = chairID; // Set Tag to chair ID for later reference

                    // Set color based on chairStatus
                    if (chairStatus == "empty")
                        seatButton.Background = Brushes.LightGreen;
                    else if (chairStatus == "booked")
                        seatButton.Background = Brushes.Red;

                    // Add button to grid
                    Grid.SetColumn(seatButton, j);
                    Grid.SetRow(seatButton, i);
                    grid.Children.Add(seatButton);
                }
            }

            // Add the grid to the main window
            mainGrid.Children.Add(grid);
        }
    }
}
