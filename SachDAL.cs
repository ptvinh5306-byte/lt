using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLNS;
using Entity;

namespace DAL_QLNS
{
    public class SachDAL
    {
        public DataTable GetList()
        {
            // Câu lệnh SQL lấy dữ liệu kèm theo tên loại từ bảng liên kết
            string query = "SELECT s.ma_sach as [Mã Sách], s.ten_sach as [Tên Sách], l.ten_loai_sach as [Loại Sách], " +
                           "s.tac_gia as [Tác Giả], s.so_luong as [Số Lượng], s.gia_ban as [Giá Bán] " +
                           "FROM tbl_Sach s, tbl_Loai_Sach l " +
                           "WHERE s.ma_loai_sach = l.ma_loai_sach";
            return DataProvider.Instance.execQuery(query);
        }

        // Hàm thêm sách gọi Proc
        public int Insert(SachEntity sach)
        {
            string query = string.Format("EXEC proc_them_sach @tenSach = N'{0}', @maLoaiSach = {1}, @tacGia = N'{2}', @soLuong = {3}, @giaBan = {4}",
                sach.TenSach, sach.MaLoaiSach, sach.TacGia, sach.SoLuong, sach.GiaBan);
            return DataProvider.Instance.execNonQuery(query);
        }

        // Hàm cập nhật sách gọi Proc
        public int Update(SachEntity sach)
        {
            string query = string.Format("EXEC proc_cap_nhat_sach @maSach = {0}, @tenSach = N'{1}', @maLoaiSach = {2}, @tacGia = N'{3}', @soLuong = {4}, @giaBan = {5}",
                sach.MaSach, sach.TenSach, sach.MaLoaiSach, sach.TacGia, sach.SoLuong, sach.GiaBan);
            return DataProvider.Instance.execNonQuery(query);
        }

        // Hàm xóa sách
        public int Delete(int maSach)
        {
            string query = "DELETE FROM tbl_Sach WHERE ma_sach = " + maSach;
            return DataProvider.Instance.execNonQuery(query);
        }
    }
}
