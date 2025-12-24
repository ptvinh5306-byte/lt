using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLNS
{
    public class ChiTietHoaDonDAL
    {
        // Lấy danh sách sách đã mua của 1 hóa đơn
        public DataTable GetDetails(int maHD)
        {
            string query = string.Format(
                "SELECT s.ma_sach, s.ten_sach AS [Tên Sách], ct.so_luong_mua AS [Số Lượng], ct.don_gia_ban AS [Đơn Giá] " +
                "FROM tbl_ChiTietHoaDon ct, tbl_Sach s " +
                "WHERE ct.ma_sach = s.ma_sach AND ct.ma_hoa_don = {0}", maHD);
            return DataProvider.Instance.execQuery(query);
        }

        // Thêm sách và tự động trừ số lượng tồn trong bảng Sách
        public int Insert(ChiTietHoaDonEntity ct)
        {
            // Lệnh 1: Thêm chi tiết
            string q1 = string.Format("INSERT INTO tbl_ChiTietHoaDon VALUES ({0}, {1}, {2}, {3})",
                                      ct.MaHoaDon, ct.MaSach, ct.SoLuongMua, ct.DonGiaBan);
            // Lệnh 2: Cập nhật giảm kho
            string q2 = string.Format("UPDATE tbl_Sach SET so_luong = so_luong - {0} WHERE ma_sach = {1}",
                                      ct.SoLuongMua, ct.MaSach);

            DataProvider.Instance.execNonQuery(q1);
            return DataProvider.Instance.execNonQuery(q2);
        }

        public int Delete(int maHD, int maS, int soLuongTraLai)
        {
            // Lệnh 1: Xoá món hàng khỏi chi tiết hóa đơn
            string q1 = string.Format("DELETE FROM tbl_ChiTietHoaDon WHERE ma_hoa_don = {0} AND ma_sach = {1}", maHD, maS);

            // Lệnh 2: Cộng trả lại số lượng vào kho sách để đảm bảo tồn kho chính xác
            string q2 = string.Format("UPDATE tbl_Sach SET so_luong = so_luong + {0} WHERE ma_sach = {1}", soLuongTraLai, maS);

            DataProvider.Instance.execNonQuery(q1);
            return DataProvider.Instance.execNonQuery(q2);
        }
    }
}
