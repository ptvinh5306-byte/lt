using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using BLL_QLNS;
using Entity;

namespace QLNS
{
    public partial class Form1 : Form
    {
        // 1. KHAI BÁO BLL
        private SachBLL sachBLL = new SachBLL();
        private LoaiSachBLL loaiBLL = new LoaiSachBLL();
        private KhachHangBLL khBLL = new KhachHangBLL();
        private HoaDonBLL hdBLL = new HoaDonBLL();
        private ChiTietHoaDonBLL chiTietBLL = new ChiTietHoaDonBLL();
        // 2. BIẾN LƯU TRỮ ID TẠM THỜI
        private int maSachLoaiSach;
        private int maSachSach;
        private int maLoaiSachLoaiSach;
        
        private int KhachHangMaKhachHang;
        private int giaBanHienTai = 0;
        private int soLuongTonHienTai = 0;
        private int maHoaDonDuocChon = 0;
        private int maSachChiTietDuocChon = 0;
        private int soLuongMuonXoa = 0;
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

        
        // TAB 1: SÁCH
        
        private void initSach()
        {
            loadDgSach();
            loadcbSachLoaiSach();
        }

        private void loadDgSach()
        {

            dgSach.DataSource = sachBLL.LayDanhSach();
            if (dgSach.ColumnCount > 0)
            {
                dgSach.Columns[0].HeaderText = "Mã Sách";
                dgSach.Columns[1].HeaderText = "Tên Sách";
                dgSach.Columns[2].HeaderText = "Loại Sách";
                dgSach.Columns[3].HeaderText = "Tác Giả";
                dgSach.Columns[4].HeaderText = "Số Lượng";
                dgSach.Columns[5].HeaderText = "Giá Bán";
            }
        }

        private void loadcbSachLoaiSach()
        {
            cbSachLoaiSach.DataSource = loaiBLL.LayDanhSach();
            cbSachLoaiSach.DisplayMember = "ten_loai_sach";
            cbSachLoaiSach.ValueMember = "ma_loai_sach";
            
        }

        private void dgSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == dgSach.RowCount - 1) return;
            DataGridViewRow row = dgSach.Rows[e.RowIndex];

            maSachSach = int.Parse(row.Cells[0].Value.ToString());
            txtSachTenSach.Text = row.Cells[1].Value.ToString();
            cbSachLoaiSach.Text = row.Cells[2].Value.ToString();
            txtSachTacGia.Text = row.Cells[3].Value.ToString();
            numSachSoLuong.Value = Convert.ToDecimal(row.Cells[4].Value);
            numSachGiaBan.Value = Convert.ToDecimal(row.Cells[5].Value);

            // Cập nhật mã loại hiện tại
            maSachLoaiSach = loaiBLL.LayMaLoai(cbSachLoaiSach.Text);
        }

        private void btnSachThem_Click(object sender, EventArgs e)
        {
            SachEntity s = new SachEntity();
            s.TenSach = txtSachTenSach.Text;
            s.MaLoaiSach = maSachLoaiSach;
            s.TacGia = txtSachTacGia.Text;
            s.SoLuong = (int)numSachSoLuong.Value;
            s.GiaBan = (int)numSachGiaBan.Value;

            if (sachBLL.ThemSach(s)) { loadDgSach(); MessageBox.Show("Thêm thành công!"); }
            else MessageBox.Show("Thêm thất bại!");
        }

        private void btnSachSua_Click(object sender, EventArgs e)
        {
            SachEntity s = new SachEntity();
            s.MaSach = maSachSach;
            s.TenSach = txtSachTenSach.Text;
            s.MaLoaiSach = maSachLoaiSach;
            s.TacGia = txtSachTacGia.Text;
            s.SoLuong = (int)numSachSoLuong.Value;
            s.GiaBan = (int)numSachGiaBan.Value;

            if (sachBLL.SuaSach(s)) { loadDgSach(); MessageBox.Show("Sửa thành công!"); }
            else MessageBox.Show("Sửa thất bại!");
        }

        private void btnSachXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa sách này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (sachBLL.XoaSach(maSachSach)) { loadDgSach(); MessageBox.Show("Xóa thành công!"); }
            }
        }

        private void cbSachLoaiSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbSachLoaiSach.SelectedValue != null)
                {
                    int.TryParse(cbSachLoaiSach.SelectedValue.ToString(), out maSachLoaiSach);
                }
            }
            catch { }
        }

        
        // TAB 2: LOẠI SÁCH 
        
        private void initLoaiSach() 
        { 
            loadDgLoaiSach(); 
        }
        private void loadDgLoaiSach() 
        {
            dgLoaiSach.DataSource = loaiBLL.LayDanhSach();
            if (dgLoaiSach.ColumnCount > 0)
            {
                dgLoaiSach.Columns[0].HeaderText = "Mã Loại";
                dgLoaiSach.Columns[1].HeaderText = "Tên Loại Sách";
            }
        }

        private void dgLoaiSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == dgLoaiSach.RowCount - 1) return;
            DataGridViewRow row = dgLoaiSach.Rows[e.RowIndex];
            maLoaiSachLoaiSach = int.Parse(row.Cells[0].Value.ToString());
            txtLoaiSachTenLoaiSach.Text = row.Cells[1].Value.ToString();
        }

        private void btnLoaiSachThem_Click(object sender, EventArgs e)
        {
            if (loaiBLL.Them(txtLoaiSachTenLoaiSach.Text))
            {
                loadDgLoaiSach(); loadcbSachLoaiSach();
                MessageBox.Show("Thêm loại thành công!");
            }
        }

        private void btnLoaiSachSua_Click(object sender, EventArgs e)
        {
            if (loaiBLL.Sua(maLoaiSachLoaiSach, txtLoaiSachTenLoaiSach.Text))
            {
                loadDgLoaiSach(); loadcbSachLoaiSach();
                MessageBox.Show("Sửa loại thành công!");
            }
        }

        private void btnLoaiSachXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa loại này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (loaiBLL.Xoa(maLoaiSachLoaiSach))
                {
                    loadDgLoaiSach(); loadcbSachLoaiSach();
                    MessageBox.Show("Xóa thành công!");
                }
            }
        }
        


        // TAB 3: HÓA ĐƠN 

        private void initHoaDon() 
        { 
            loadDgHoaDon();
            loadcbChonSach();
        }
        private void loadDgHoaDon() 
        {           
            dgHoaDon.DataSource = hdBLL.LayDanhSach();
            if (dgHoaDon.ColumnCount > 0)
            {
                dgHoaDon.Columns[0].HeaderText = "Mã Hóa Đơn";
                dgHoaDon.Columns[1].HeaderText = "Tên Khách Hàng";
                dgHoaDon.Columns[2].HeaderText = "Số Điện Thoại";
                dgHoaDon.Columns[3].HeaderText = "Ngày Lập";
            }
        }

        private void dgHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nhấn đúng hàng dữ liệu, không phải hàng trống hoặc tiêu đề
            if (e.RowIndex < 0 || e.RowIndex >= dgHoaDon.Rows.Count - 1) return;

            DataGridViewRow row = dgHoaDon.Rows[e.RowIndex];
            maHoaDonDuocChon = Convert.ToInt32(row.Cells[0].Value);
            txtHoaDonTenKhachHang.Text = row.Cells[1].Value.ToString();
            txtHoaDonSĐTKhachHang.Text = row.Cells[2].Value.ToString();

            loadDgChiTiet(maHoaDonDuocChon);

            // Xử lý Ngày Lập an toàn để tránh lỗi DateTime
            object valDate = row.Cells[3].Value;
            if (valDate != null && DateTime.TryParse(valDate.ToString(), out DateTime dt))
            {
                dateNgayLapHoaDon.Value = dt;
            }
        }

        private void btnHoaDonThem_Click(object sender, EventArgs e)
        {
            if (hdBLL.Them(dateNgayLapHoaDon.Value, txtHoaDonTenKhachHang.Text, txtHoaDonSĐTKhachHang.Text))
            {
                loadDgHoaDon(); MessageBox.Show("Thêm hóa đơn thành công!");
            }
        }

        private void btnHoaDonSua_Click(object sender, EventArgs e)
        {
            if (hdBLL.Sua(maHoaDonDuocChon, dateNgayLapHoaDon.Value, txtHoaDonTenKhachHang.Text, txtHoaDonSĐTKhachHang.Text))
            {
                loadDgHoaDon(); MessageBox.Show("Sửa hóa đơn thành công!");
            }
        }

        private void btnHoaDonXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa hóa đơn này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (hdBLL.Xoa(maHoaDonDuocChon)) { loadDgHoaDon(); MessageBox.Show("Xóa thành công!"); }
            }
        }

        private void txtHoaDonSĐTKhachHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }
        private void btnThemSach_Click(object sender, EventArgs e)
        {
            // 1.Kiểm tra đã chọn hóa đơn chưa
            if (maHoaDonDuocChon == 0)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn ở bảng trên trước!", "Thông báo");
                return;
            }

            // 2. Tạo đối tượng và gán dữ liệu
            ChiTietHoaDonEntity ct = new ChiTietHoaDonEntity();
            ct.MaHoaDon = maHoaDonDuocChon;
            ct.MaSach = Convert.ToInt32(cbChonSach.SelectedValue);
            ct.SoLuongMua = (int)numSoLuongMua.Value; // Đảm bảo bạn đã đặt tên Numeric là numSoLuongMua
            ct.DonGiaBan = giaBanHienTai;

            // 3. Gọi BLL xử lý nghiệp vụ kiểm tra tồn và trừ kho
            if (chiTietBLL.ThemMặtHàng(ct, soLuongTonHienTai))
            {
                MessageBox.Show("Thêm sách vào hóa đơn thành công!");

                // 4. Cập nhật lại giao diện
                loadDgChiTiet(maHoaDonDuocChon); // Refresh bảng dưới
                loadDgSach(); // Refresh bảng sách ở Tab 1 để thấy kho giảm
            }
            else
            {
                MessageBox.Show("Kho không đủ hàng hoặc lỗi hệ thống!", "Cảnh báo");
            }
        }
        private void cbChonSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChonSach.SelectedValue != null && cbChonSach.SelectedIndex != -1)
            {
                // Giả sử SelectedItem trả về một dòng dữ liệu hoặc bạn truy vấn lại từ bảng Sách
                // Ở đây đơn giản nhất là bạn lấy dữ liệu từ bảng sách đã load lên
                DataRowView row = (DataRowView)cbChonSach.SelectedItem;
                giaBanHienTai = Convert.ToInt32(row["Giá Bán"]);
                soLuongTonHienTai = Convert.ToInt32(row["Số Lượng"]);
            }
        }
        private void loadcbChonSach()
        {
            DataTable dt = sachBLL.LayDanhSach();

            // 1. Thiết lập các thuộc tính TRƯỚC
            cbChonSach.DisplayMember = "Tên Sách";
            cbChonSach.ValueMember = "Mã Sách";

            // 2. Gán nguồn dữ liệu SAU CÙNG
            cbChonSach.DataSource = dt;

            cbChonSach.SelectedIndex = -1;
        }
        private void dgChiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu click vào hàng hợp lệ
            if (e.RowIndex < 0 || e.RowIndex >= dgChiTiet.Rows.Count - 1) return;

            DataGridViewRow row = dgChiTiet.Rows[e.RowIndex];

            // Lấy Mã Sách ẩn (đã thêm vào DAL ở bước trước) để phục vụ việc Xoá/Sửa
            maSachChiTietDuocChon = Convert.ToInt32(row.Cells[0].Value);
            soLuongMuonXoa = Convert.ToInt32(row.Cells[2].Value);

            // Hiển thị lại thông tin lên các ô nhập liệu để người dùng dễ quan sát
            cbChonSach.Text = row.Cells[1].Value.ToString();
            numSoLuongMua.Value = Convert.ToInt32(row.Cells[2].Value);
        }
        private void btnXoaChiTiet_Click(object sender, EventArgs e)
        {
            if (maSachChiTietDuocChon == 0)
    {
        MessageBox.Show("Vui lòng chọn món hàng trong bảng chi tiết cần xoá!");
        return;
    }

    if (MessageBox.Show("Bạn có chắc chắn muốn xoá món hàng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
    {
        // Gọi xuống BLL/DAL để thực hiện xoá và trả lại kho
        if (chiTietBLL.XoaMatHang(maHoaDonDuocChon, maSachChiTietDuocChon, soLuongMuonXoa))
        {
            MessageBox.Show("Đã xoá thành công và cập nhật lại kho!");
            
            // Cập nhật lại giao diện
            loadDgChiTiet(maHoaDonDuocChon); // Nạp lại bảng chi tiết và tự động tính lại tổng tiền
            loadDgSach(); // Nạp lại bảng sách ở Tab 1 để thấy số lượng tồn đã tăng lên
            
            // Reset biến tạm
            maSachChiTietDuocChon = 0;
        }
        else
        {
            MessageBox.Show("Lỗi khi xoá món hàng!");
        }
    }
        }
        private void loadDgChiTiet(int maHD)
        {
            // Sử dụng tham số maHD truyền vào thay vì biến chưa khai báo
            dgChiTiet.DataSource = chiTietBLL.LayDanhSach(maHD);

            if (dgChiTiet.Columns.Count > 0)
            {
                dgChiTiet.Columns[0].HeaderText = "Tên Sách";
                dgChiTiet.Columns[1].HeaderText = "Số Lượng";
                dgChiTiet.Columns[2].HeaderText = "Đơn Giá";
                dgChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            tinhTongTien();
        }
        private void tinhTongTien()
        {
            long tongTien = 0;
            foreach (DataGridViewRow row in dgChiTiet.Rows)
            {
                // row.IsNewRow giúp bỏ qua hàng trống cuối cùng của bảng
                if (!row.IsNewRow && row.Cells[2].Value != null && row.Cells[2].Value != null)
                {
                    // Cột 1 là Số lượng, Cột 2 là Đơn giá
                    int sl = Convert.ToInt32(row.Cells[2].Value);
                    int dg = Convert.ToInt32(row.Cells[3].Value);
                    tongTien += (long)sl * dg;
                }
            }
            // Gán kết quả vào Label
            lblTongTien.Text = "Tổng tiền: " + tongTien.ToString("N0") + " VNĐ";
        }

        // TAB 4: KHÁCH HÀNG 

        private void initKhachHang() 
        { 
            loadDgKhachHang(); 
        }
        private void loadDgKhachHang() 
        {            
            dgKhachHang.DataSource = khBLL.LayDanhSach();
            if (dgKhachHang.ColumnCount > 0)
            {
                dgKhachHang.Columns[0].HeaderText = "Mã KH";
                dgKhachHang.Columns[1].HeaderText = "Tên Khách Hàng";
                dgKhachHang.Columns[2].HeaderText = "Số Điện Thoại";
                dgKhachHang.Columns[3].HeaderText = "Địa Chỉ";
            }
        }

        private void dgKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == dgKhachHang.RowCount - 1) return;
            DataGridViewRow row = dgKhachHang.Rows[e.RowIndex];
            KhachHangMaKhachHang = int.Parse(row.Cells[0].Value.ToString());
            txtKhachHangTenKhachHang.Text = row.Cells[1].Value.ToString();
            txtKhachHangSĐTKhachHang.Text = row.Cells[2].Value.ToString();
            txtKhachHangDiaChi.Text = row.Cells[3].Value.ToString();
        }

        private void btnKhachHangThem_Click(object sender, EventArgs e)
        {
            if (khBLL.Them(txtKhachHangTenKhachHang.Text, txtKhachHangSĐTKhachHang.Text, txtKhachHangDiaChi.Text))
            {
                loadDgKhachHang(); MessageBox.Show("Thêm khách hàng thành công!");
            }
        }

        private void btnKhachHangSua_Click(object sender, EventArgs e)
        {
            if (khBLL.Sua(KhachHangMaKhachHang, txtKhachHangTenKhachHang.Text, txtKhachHangSĐTKhachHang.Text, txtKhachHangDiaChi.Text))
            {
                loadDgKhachHang(); MessageBox.Show("Sửa khách hàng thành công!");
            }
        }

        private void btnKhachHangXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa khách hàng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (khBLL.Xoa(KhachHangMaKhachHang)) { loadDgKhachHang(); MessageBox.Show("Xóa thành công!"); }
            }
        }

       
    }
}