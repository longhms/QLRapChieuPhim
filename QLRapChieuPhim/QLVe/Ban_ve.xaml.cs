using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QLRapChieuPhim.QLVe
{
    /// <summary>
    /// Interaction logic for Ban_ve.xaml
    /// </summary>
    public partial class Ban_ve : Window
    {
        Classes.DataProcessor dataProcessor = new Classes.DataProcessor(Login.cinemaID);
        int numRows = 5;
        int numSeatsPerRow = 6;
        string maShow;
        int loaiVe = 0;
        int donGia = 0; int tongThu = 0;
        int soTienThanhToan;
        int soVe;
        string maVe;

        public Ban_ve()
        {
            InitializeComponent();
            LoadData();
            DrawSeats();
        }
        void LoadData()
        {
            DataTable dtLC = dataProcessor.ReadData("SELECT maShow FROM tblBuoiChieu");
            cboLichChieu.ItemsSource = dtLC.AsDataView();
            cboLichChieu.DisplayMemberPath = "maShow";
            cboLichChieu.SelectedValuePath = "maShow";

            cboHangGhe.Items.Add("A");
            cboHangGhe.Items.Add("B");
            cboHangGhe.Items.Add("C");
            cboHangGhe.Items.Add("D");
            cboHangGhe.Items.Add("E");

            for (int i = 0; i < numSeatsPerRow; i++)
            {
                cboSoGhe.Items.Add(i+1);
            }

            DataTable dt = dataProcessor.ReadData("SELECT * FROM tblMaGiamGia");
            cboMaGG.ItemsSource = dt.AsDataView();
            cboMaGG.DisplayMemberPath = "tenMaGiamGia";
            cboMaGG.SelectedValuePath = "maGiamGia";

            
        }
      

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            maShow = cboLichChieu.Text;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            
                this.Close();
        }


        private void DrawSeats()
        {
            

            DataTable dt = dataProcessor.ReadData("SELECT * FROM tblChair");

            DataTable data = dataProcessor.ReadData("SELECT hangGhe,soGhe FROM tblVe WHERE maShow = ('" + maShow + "')");

            string[] test = null;

            MessageBox.Show(data.Rows.Count.ToString());

            if (maShow != "" && data.Rows.Count > 0)
            {
                test = new string[data.Rows.Count];
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    test[i] = data.Rows[i]["hangGhe"].ToString() + data.Rows[i]["soGhe"].ToString();
                }

                
            }


            if (dt.Rows.Count != numRows * numSeatsPerRow)
            {
                MessageBox.Show("Dữ liệu từ cơ sở dữ liệu không khớp với số lượng ghế dự kiến.");
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



                    if (test != null)
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

        private void cboLichChieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            maShow = cboLichChieu.SelectedValue?.ToString();
            DrawSeats();

            string sql = "SELECT tP.tenPhim,maPhong,ngayChieu,maGioChieu FROM tblBuoiChieu tB INNER JOIN tblPhim tP ON tP.maPhim = tB.maPhim WHERE tB.maShow = ('"+ maShow +"')";
            
            DataTable dt = dataProcessor.ReadData(sql);
            

            lblPhim.Content = dt.Rows[0]["tenPhim"].ToString();
            lblPhongChieu.Content = dt.Rows[0]["maPhong"].ToString();
            lblNgayChieu.Content = dt.Rows[0]["ngayChieu"].ToString();
            lblGioChieu.Content = dt.Rows[0]["maGioChieu"].ToString();
            string tmp = dt.Rows[0]["maGioChieu"].ToString();
            DateTime tmpDT = DateTime.Parse(tmp);
            int tmpInt = tmpDT.Hour;
            if (tmpInt < 10)
            {
                tmp = "0"+ tmpInt.ToString() +":00";
            }
            else
            {
                tmp = tmpInt.ToString() +":00";
            }

            DataTable dtDG = dataProcessor.ReadData("SELECT * FROM tblGioChieu WHERE maGioChieu =('" + tmp + "')");

            donGia = int.Parse( dtDG.Rows[0]["donGia"].ToString() );

            
        }

        private void rdo_Checked(object sender, RoutedEventArgs e)
        {
            if (rdoNguoiLon.IsChecked == true)
            {
                loaiVe = 20000;
            }
            else if (rdoSinhVien.IsChecked == true)
            {
                loaiVe = 10000;
            }
            else if (rdoTreEm.IsChecked == true)
            {
                loaiVe = 0;
            }
        }

        private void btnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = dataProcessor.ReadData("SELECT maVe FROM tblVe");
            
            int[] tmpMV = new int[dt.Rows.Count];
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                string number = dt.Rows[i]["maVe"].ToString();
                string numberr = number.Substring(1);
                tmpMV[i] = int.Parse(numberr);
            }

            string lichChieu = cboLichChieu?.Text;
            string hangGhe = cboHangGhe?.Text;
            string soGhe = cboSoGhe?.Text;
            

            if (soGhe == null || hangGhe == null || lichChieu == null)
            {
                MessageBox.Show("Hãy chọn đủ thông tin cho khách hàng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } 

            

            if(rdoNguoiLon.IsChecked != true && rdoSinhVien.IsChecked != true && rdoTreEm.IsChecked != true)
            {
                MessageBox.Show("Hãy chọn loại vé cho khách hàng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataTable dataTable = dataProcessor.ReadData("SELECT maShow,hangGhe,soGhe FROM tblVe WHERE maShow =('" + lichChieu + "') AND hangGhe = ('"+ hangGhe +"') AND soGhe = ('"+ soGhe +"')");
            if(dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Có vẻ chỗ này đã có người đặt trước!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataTable data = dataProcessor.ReadData("SELECT maShow,tB.maPhim,tP.tongThu,soVedaban,tongTien FROM tblBuoiChieu tB INNER JOIN tblPhim tP ON tP.maPhim = tB.maPhim WHERE maShow = ('" + lichChieu + "')");
            int tongThu = int.Parse(data.Rows[0]["tongThu"].ToString());
            int soVedaban = int.Parse(data.Rows[0]["tongThu"].ToString());
            int tongTien = int.Parse(data.Rows[0]["tongTien"].ToString());
            string maPhim = data.Rows[0]["maPhim"].ToString();


            if (MessageBox.Show("Bạn có chắc chắn muốn đặt vé này?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Random rnd = new Random();
                do
                {
                    soVe = rnd.Next(100);
                } while (tmpMV.Contains(soVe));

                maVe = "V"+ soVe.ToString();
                tongThu += int.Parse(txbThanhToan.Text);
                soVedaban += 1;
                tongTien += int.Parse(txbThanhToan.Text);

                dataProcessor.ChangeData("INSERT INTO tblVe values('"+ lichChieu +"','"+ maVe +"','"+ hangGhe +"','"+ soGhe +"')");
                dataProcessor.ChangeData("UPDATE tblPhim SET tongThu = ('" + tongThu + "') WHERE maPhim = '"+ maPhim +"'");
                dataProcessor.ChangeData("UPDATE tblBuoiChieu SET soVedaban = ('" + soVedaban + "') WHERE maShow = '" + lichChieu + "'");
                dataProcessor.ChangeData("UPDATE tblBuoiChieu SET tongTien = ('" + tongTien + "') WHERE maShow = '" + lichChieu + "'");
                MessageBox.Show("Bạn đã bán được một vé xem phim!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                Refresh();
            }
        }

        void Refresh()
        {
            cboHangGhe.Text = "";
            cboSoGhe.Text = "";
            cboMaGG.Text = "";
            /*cboLichChieu.SelectedItem = null;*/
            DrawSeats();
            rdoNguoiLon.IsChecked = false;
            rdoSinhVien.IsChecked = false;
            rdoTreEm.IsChecked = false;
            btnXacNhan.IsEnabled = false;
        }

        private void btnThanhTien_Click(object sender, RoutedEventArgs e)
        {
            tongThu = donGia + loaiVe;
            txbTongTien.Text = tongThu.ToString();
            txbMaGG.Text = cboMaGG.Text;
            int tmpMGG;
            if (int.TryParse(cboMaGG.SelectedValue?.ToString(), out tmpMGG))
            {
                tmpMGG = int.Parse(cboMaGG.SelectedValue.ToString());
                soTienThanhToan = tongThu - ((tongThu / 100) * tmpMGG);
            }
            else
            {
                tmpMGG = -1;
                soTienThanhToan = tongThu;
            }
            txbThanhToan.Text = soTienThanhToan.ToString();
            btnXacNhan.IsEnabled = true;
        }

        private void btnRf_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
