using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS
{
    using DAL_QLNS;
    using BLL_QLNS;
    public partial class Form1 : Form
    {
        private DataProvider dataProvider = new DataProvider();
        private int maSachLoaiSach;
        private int maSachSach;
        private int maLoaiSachLoaiSach;
        private int maHoaDonHoaDon;
        private int KhachHangMaKhachHang;
        

        public Form1()
        {
            InitializeComponent();
            init();
        }  
        
        private void init()
        {
            initSach();
            initLoaiSach();
            initHoaDon();
            initKhachHang();
        }


        // Xử lý sách

        private void initSach()
        {
            loadDgSach();
            loadcbSachLoaiSach();
        }

        private void loadDgSach()
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder("SELECT ma_sach as [Mã Sách]");
            query.Append(", ten_sach as [Tên Sách]");
            query.Append(",ten_loai_sach as [Loại Sách]");
            query.Append(",tac_gia as [Tác Giả]");
            query.Append(",so_luong as [Số Lượng]");
            query.Append(",gia_ban as [Giá Bán]");
            query.Append(" FROM tbl_Sach, tbl_Loai_Sach");
            query.Append(" WHERE tbl_Sach.ma_loai_sach = tbl_Loai_Sach.ma_loai_sach;");


            dt = dataProvider.execQuery(query.ToString());
            dgSach.DataSource = dt;
        }

        private void loadcbSachLoaiSach()
        {
            DataTable dt = new DataTable();
            dt = dataProvider.execQuery("SELECT * FROM tbl_loai_sach");
            cbSachLoaiSach.DisplayMember = "ten_loai_sach";
            cbSachLoaiSach.ValueMember = "ma_loai_sach";
            cbSachLoaiSach.DataSource = dt;      
        }


        private void dgSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowId = e.RowIndex;
            if (rowId < 0) rowId = 0;
            if (rowId == dgSach.RowCount - 1) rowId = rowId - 1;

            DataGridViewRow row = dgSach.Rows[rowId];
            maSachSach = (int)row.Cells[0].Value;
            txtSachTenSach.Text = row.Cells[1].Value.ToString();
            cbSachLoaiSach.Text = row.Cells[2].Value.ToString();
            txtSachTacGia.Text = row.Cells[3].Value.ToString();
            numSachSoLuong.Value = Convert.ToDecimal(row.Cells[4].Value);
            numSachGiaBan.Value = Convert.ToDecimal(row.Cells[5].Value);

            maSachLoaiSach = (int)dataProvider.execScaler("SELECT ma_loai_sach FROM tbl_loai_sach WHERE ten_loai_sach = N'" + cbSachLoaiSach.Text + "'");
        }

        private void btnSachThem_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("EXEC proc_them_sach");
            query.Append(" @tenSach = N'" + txtSachTenSach.Text + "'");
            query.Append(",@maLoaiSach = " + maSachLoaiSach);
            query.Append(",@tacGia = N'" + txtSachTacGia.Text + "'");
            query.Append(",@soLuong = " + numSachSoLuong.Value);
            query.Append(",@giaBan = " + numSachGiaBan.Value);

            int result = dataProvider.execNonQuery(query.ToString());

            if (result > 0)
            {
                loadDgSach();
                MessageBox.Show("Thêm sách thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
             
            }
            else
            {
                MessageBox.Show("Thêm sách không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSachSua_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("EXEC proc_cap_nhat_sach");
            query.Append(" @maSach = " + maSachSach);
            query.Append(",@tenSach = N'" + txtSachTenSach.Text + "'");
            query.Append(",@maLoaiSach = " + maSachLoaiSach);
            query.Append(",@tacGia = N'" + txtSachTacGia.Text + "'");
            query.Append(",@soLuong = " + numSachSoLuong.Value);
            query.Append(",@giaBan = " + numSachGiaBan.Value);

            int result = dataProvider.execNonQuery(query.ToString());

            if (result > 0)
            {
                loadDgSach();
                MessageBox.Show("Cập nhật sách thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show("Cập Nhật sách không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSachXoa_Click(object sender, EventArgs e)
        {
           DialogResult check = MessageBox.Show("Bạn có chắc chắn muốn xoá sách" + txtSachTenSach.Text + "?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                string query = "DELETE FROM tbl_sach WHERE ma_sach = " + maSachSach;
                int result = dataProvider.execNonQuery(query.ToString());

                if (result > 0)
                {
                    loadDgSach();
                    MessageBox.Show("Xoá sách thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {
                    MessageBox.Show("Xoá sách không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void cbSachLoaiSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            maSachLoaiSach = (int)comboBox.SelectedValue;

        }


        // Xử lý loại sách
        private void initLoaiSach()
        {
            loadDgLoaiSach();
        }

        private void loadDgLoaiSach()
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder("SELECT ma_loai_sach as [Mã Loại Sách]");
            query.Append(",ten_loai_sach as [Tên Loại Sách]");
            query.Append(" FROM tbl_Loai_Sach");
            
            dt = dataProvider.execQuery(query.ToString());

            dgLoaiSach.DataSource = dt;

        }

        private void dgLoaiSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowId = e.RowIndex;
            if (rowId < 0) rowId = 0;
            if (rowId == dgLoaiSach.RowCount - 1) rowId = rowId - 1;

            DataGridViewRow row = dgLoaiSach.Rows[rowId];
            maLoaiSachLoaiSach = (int)row.Cells[0].Value;
            txtLoaiSachTenLoaiSach.Text = row.Cells[1].Value.ToString();
        }

        private void btnLoaiSachThem_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("EXEC proc_them_loai_sach");
            query.Append(" @tenLoaiSach = N'" + txtLoaiSachTenLoaiSach.Text + "'");
            
            int result = dataProvider.execNonQuery(query.ToString());

            if (result > 0)
            {
                loadDgLoaiSach();
                loadcbSachLoaiSach();
                MessageBox.Show("Thêm loại sách thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show("Thêm loại sách không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoaiSachSua_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("EXEC proc_cap_nhat_loai_sach");
            query.Append(" @tenLoaiSach = N'" + txtLoaiSachTenLoaiSach.Text + "'");
            query.Append(",@maLoaiSach = " + maLoaiSachLoaiSach);
            

            int result = dataProvider.execNonQuery(query.ToString());

            if (result > 0)
            {
                loadDgLoaiSach();
                loadcbSachLoaiSach();
                MessageBox.Show("Cập nhật sách thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show("Cập Nhật sách không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoaiSachXoa_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Bạn có chắc chắn muốn xoá loại sách" + txtLoaiSachTenLoaiSach.Text + "?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                string query = "DELETE FROM tbl_loai_sach WHERE ma_loai_sach = " + maLoaiSachLoaiSach;
                int result = dataProvider.execNonQuery(query.ToString());

                if (result > 0)
                {
                    loadDgLoaiSach();
                    loadcbSachLoaiSach();
                    MessageBox.Show("Xoá loại sách thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {
                    MessageBox.Show("Xoá loại sách không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        // Xử lý hoá đơn
        private void initHoaDon()
        {
            loadDgHoaDon();
        }
        private void loadDgHoaDon()
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder("SELECT ma_hoa_don as [Mã Hoá Đơn]");
            query.Append(",ngay_lap_hoa_don as [Ngày Lập Hoá Đơn]");
            query.Append(",ten_khach_hang as [Tên Khách Hàng]");
            query.Append(",sdt_khach_hang as [Số Điện Thoại Khách Hàng]");
            query.Append(" FROM tbl_hoa_don");

            dt = dataProvider.execQuery(query.ToString());

            dgHoaDon.DataSource = dt;
        }

        private void dgHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowId = e.RowIndex;
            if (rowId < 0) rowId = 0;
            if (rowId == dgHoaDon.RowCount - 1) rowId = rowId - 1;
            DataGridViewRow row = dgHoaDon.Rows[rowId];

            maHoaDonHoaDon = (int)row.Cells[0].Value;
            dateNgayLapHoaDon.Value = DateTime.Parse(row.Cells[1].Value.ToString());
            txtHoaDonTenKhachHang.Text = row.Cells[2].Value.ToString();
            txtHoaDonSĐTKhachHang.Text = row.Cells[3].Value.ToString();
            
        }

        private void btnHoaDonThem_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("EXEC proc_them_hoa_don");
            query.Append(" @ngayLapHoaDon =  '" + dateNgayLapHoaDon.Value+ "'");
            query.Append(", @tenKhachHang =  N'" + txtHoaDonTenKhachHang.Text + "'");
            query.Append(", @sdtKhachHang =  '" + txtHoaDonSĐTKhachHang.Text + "'");


            int result = dataProvider.execNonQuery(query.ToString());

            if (result > 0)
            {
                loadDgHoaDon();
                
                MessageBox.Show("Thêm hoá đơn thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show("Thêm hoá đơn không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHoaDonSua_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("EXEC proc_cap_nhat_hoa_don");
            query.Append(" @ngayLapHoaDon =  '" + dateNgayLapHoaDon.Value + "'");
            query.Append(", @tenKhachHang =  N'" + txtHoaDonTenKhachHang.Text + "'");
            query.Append(", @sdtKhachHang =  '" + txtHoaDonSĐTKhachHang.Text + "'");
            query.Append(", @maHoaDon =  " + maHoaDonHoaDon);


            int result = dataProvider.execNonQuery(query.ToString());

            if (result > 0)
            {
                loadDgHoaDon();
                
                MessageBox.Show("Cập nhật hoá đơn thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show("Cập Nhật hoá đơn không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHoaDonXoa_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Bạn có chắc chắn muốn xoá hoá đơn sách" + maHoaDonHoaDon + "?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                string query = "DELETE FROM tbl_hoa_don WHERE ma_hoa_don = " + maHoaDonHoaDon;
                int result = dataProvider.execNonQuery(query.ToString());

                if (result > 0)
                {
                    loadDgHoaDon();
                    ;
                    MessageBox.Show("Xoá hoá đơn thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {
                    MessageBox.Show("Xoá hoá đơn không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);               
                }
        }
    }

        private void txtHoaDonSĐTKhachHang_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            { 
                e.Handled = true;
            }
        }


        // Xử lý Khách Hàng

        private void initKhachHang()
        {
            loadDgKhachHang();
        }
        private void loadDgKhachHang()
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder("SELECT ma_khach_hang as [Mã Khách Hàng]");
            query.Append(",ten_khach_hang as [Tên Khách Hàng]");
            query.Append(",sdt_khach_hang as [Số Điện Thoại Khách Hàng]");
            query.Append(",dia_chi as [Địa Chỉ]");
            query.Append(" FROM tbl_khach_hang");

            dt = dataProvider.execQuery(query.ToString());

            dgKhachHang.DataSource = dt;
        }

        private void dgKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowId = e.RowIndex;
            if (rowId < 0) rowId = 0;
            if (rowId == dgKhachHang.RowCount - 1) rowId = rowId - 1;
            DataGridViewRow row = dgKhachHang.Rows[rowId];

            KhachHangMaKhachHang = (int)row.Cells[0].Value;
            txtKhachHangTenKhachHang.Text = row.Cells[1].Value.ToString();
            txtKhachHangSĐTKhachHang.Text = row.Cells[2].Value.ToString();
            txtKhachHangDiaChi.Text = row.Cells[3].Value.ToString();
        }

        private void btnKhachHangThem_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("EXEC proc_them_khach_hang");
            query.Append(" @tenKhachHang =  N'" + txtKhachHangTenKhachHang.Text + "'");
            query.Append(", @sdtKhachHang = N'" + txtKhachHangSĐTKhachHang.Text + "'");
            query.Append(", @diachi = N'" + txtKhachHangDiaChi.Text + "'");
            //query.Append(", @maKhachHang =  " + KhachHangMaKhachHang);


            int result = dataProvider.execNonQuery(query.ToString());

            if (result > 0)
            {
                loadDgKhachHang();

                MessageBox.Show("Thêm khách hàng thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show("Thêm khách hàng không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKhachHangSua_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("EXEC proc_cap_nhat_khach_hang");
            query.Append(" @tenKhachHang =  N'" + txtKhachHangTenKhachHang.Text + "'");
            query.Append(", @sdtKhachHang = N'" + txtKhachHangSĐTKhachHang.Text + "'");
            query.Append(", @diachi = N'" + txtKhachHangDiaChi.Text + "'");
            query.Append(", @makhachhang =  " + KhachHangMaKhachHang);

            int result = dataProvider.execNonQuery(query.ToString());

            if (result > 0)
            {
                loadDgKhachHang();

                MessageBox.Show("Cập nhật khách hàng thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else
            {
                MessageBox.Show("Cập Nhật khách hàng không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKhachHangXoa_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Bạn có chắc chắn muốn xoá khách hàng" + KhachHangMaKhachHang + "?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                string query = "DELETE FROM tbl_khach_hang WHERE ma_khach_hang = " + KhachHangMaKhachHang;
                int result = dataProvider.execNonQuery(query.ToString());

                if (result > 0)
                {
                    loadDgKhachHang();
                    ;
                    MessageBox.Show("Xoá khách hàng thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {
                    MessageBox.Show("Xoá khách hàng không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
