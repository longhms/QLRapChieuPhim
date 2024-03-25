﻿using QLRapChieuPhim.Classes;
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

namespace QLRapChieuPhim.QLPhim.ChiTietPhim
{
    /// <summary>
    /// Interaction logic for Them_phim.xaml
    /// </summary>
    public partial class Them_phim : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);
        public Them_phim()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // cbo Quốc gia sản xuất
            DataTable dtQuocgia = dataProcessor.ReadData("Select * from tblQGsanXuat");
            cboQuocgia.ItemsSource = dtQuocgia.AsDataView();
            cboQuocgia.DisplayMemberPath = "tenQGSanXuat";
            cboQuocgia.SelectedValuePath = "maQGSanXuat";

            //cbo Hãng sản xuất
            DataTable dtHangSX = dataProcessor.ReadData("Select * from tblHangSX");
            cboHangSX.ItemsSource = dtHangSX.AsDataView();
            cboHangSX.DisplayMemberPath = "tenHangSX";
            cboHangSX.SelectedValuePath = "maHangSX";

            //cbo Thể loại
            DataTable dtTheLoai = dataProcessor.ReadData("Select * from tblTheLoai");
            cboTheLoai.ItemsSource = dtTheLoai.AsDataView();
            cboTheLoai.DisplayMemberPath = "tenTheLoai";
            cboTheLoai.SelectedValuePath = "maTheLoai";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtPhim = new DataTable();
            if(txtID.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã phim");
                txtID.Focus();
                return;
            }
            //Kiem tra trung ma
            dtPhim = dataProcessor.ReadData("Select * from tblPhim where maPhim = '" + txtID.Text + "'");
            if(dtPhim.Rows.Count > 0)
            {
                MessageBox.Show("Mã phim bị trùng, vui lòng nhập mã khác");
                txtID.Focus();
                return;
            }
            dataProcessor.ChangeData("insert into tblPhim values('" + txtID.Text + "','" + txtTenphim.Text + "','" + cboQuocgia.SelectedItem.ToString() + "','" + cboHangSX.SelectedItem.ToString() + "','" + txtDaodien.Text + "','" + cboTheLoai.SelectedItem.ToString() + "','" + txtNgayKC.Text + "','" + txtNgayKT.Text + "','" + txtNuDVC.Text + "','" + txtNamDVC.Text + "','" + txtNoidung.Text + "','" + txtChiPhi + "','" + txtThu.Text + "') ");
            MessageBox.Show("Thêm thành công");

            try
            {
                // Lưu dữ liệu vào cơ sở dữ liệu

                // Gọi phương thức cập nhật dữ liệu trên cửa sổ Phim
                Phim phim = Application.Current.Windows.OfType<Phim>().FirstOrDefault();
                if (phim != null)
                {
                    phim.RefreshDataGrid();
                }

                // Hiển thị thông báo lưu thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi lưu dữ liệu
            }
        }
    }
}
