﻿using QLRapChieuPhim.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QLRapChieuPhim.QLPhim.ChiTietPhim
{
    /// <summary>
    /// Interaction logic for Phim.xaml
    /// </summary>
    public partial class Phim : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        public Phim()
        {
            InitializeComponent();
        }
        public void RefreshDataGrid()
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                DataTable dt = dataProcessor.ReadData("SELECT * FROM tblPhim");
                dgPhim.ItemsSource = dt.AsDataView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
            

        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            Them_phim them_Phim = new Them_phim();
            them_Phim.ShowDialog();
        }


        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if(dgPhim.SelectedItem != null)
    {
                DataRowView selectedRow = dgPhim.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    Sua_phim suaPhim = new Sua_phim(selectedRow);
                    suaPhim.ShowDialog();
                    RefreshDataGrid();
                }
            }
    else
            {
                MessageBox.Show("Vui lòng chọn một phim để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    


        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa phim này không ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DataRowView selectedRow = dgPhim.SelectedItem as DataRowView;

                if (selectedRow != null)
                {
                    string maPhim = selectedRow["maPhim"].ToString();
                    dataProcessor.ChangeData("DELETE FROM tblPhim WHERE maPhim = '" + maPhim + "'");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một phim để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát ứng dụng?", "Thong bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
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
