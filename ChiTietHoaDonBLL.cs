using DAL_QLNS;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLNS
{
    public class ChiTietHoaDonBLL
    {
        ChiTietHoaDonDAL dal = new ChiTietHoaDonDAL();

        public DataTable LayDanhSach(int maHD) => dal.GetDetails(maHD);

        public bool ThemMặtHàng(ChiTietHoaDonEntity ct, int tonKho)
        {
            // Ràng buộc: Không được bán quá số lượng đang có
            if (ct.SoLuongMua > tonKho) return false;
            return dal.Insert(ct) > 0;
        }
        public bool XoaMatHang(int maHD, int maS, int soLuongTraLai)
        {
            // Gọi xuống hàm Delete vừa tạo ở DAL
            return dal.Delete(maHD, maS, soLuongTraLai) > 0;
        }
    }
}
