using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLNS
{
    public class HoaDonDAL
    {
        public DataTable GetList()
        {
            return DataProvider.Instance.execQuery("SELECT * FROM tbl_Hoa_Don ");
        }

        public int Insert(DateTime ngay, string ten, string sdt)
        {
            string query = string.Format("EXEC proc_them_hoa_don @ngayLapHoaDon = '{0}', @tenKhachHang = N'{1}', @sdtKhachHang = '{2}'",
                ngay.ToString("yyyy-MM-dd"), ten, sdt);
            return DataProvider.Instance.execNonQuery(query);
        }

        public int Update(int ma, DateTime ngay, string ten, string sdt)
        {
            string query = string.Format("EXEC proc_cap_nhat_hoa_don @maHoaDon = {0}, @ngayLapHoaDon = '{1}', @tenKhachHang = N'{2}', @sdtKhachHang = '{3}'",
                ma, ngay.ToString("yyyy-MM-dd"), ten, sdt);
            return DataProvider.Instance.execNonQuery(query);
        }

        public int Delete(int ma)
        {
            string query = "DELETE FROM tbl_Hoa_Don WHERE ma_hoa_don = " + ma;
            return DataProvider.Instance.execNonQuery(query);
        }
    }

}
