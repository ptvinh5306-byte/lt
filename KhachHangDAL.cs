using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLNS
{
    public class KhachHangDAL
    {
        public DataTable GetList()
        {
            return DataProvider.Instance.execQuery("SELECT * FROM tbl_Khach_Hang ");
        }

        public int Insert(string ten, string sdt, string diachi)
        {
            string query = string.Format("EXEC proc_them_khach_hang @tenKhachHang = N'{0}', @sdtKhachHang = '{1}', @diaChi = N'{2}'",
                ten, sdt, diachi);
            return DataProvider.Instance.execNonQuery(query);
        }

        public int Update(int ma, string ten, string sdt, string diachi)
        {
            string query = string.Format("EXEC proc_cap_nhat_khach_hang @maKhachHang = {0}, @tenKhachHang = N'{1}', @sdtKhachHang = '{2}', @diaChi = N'{3}'",
                ma, ten, sdt, diachi);
            return DataProvider.Instance.execNonQuery(query);
        }

        public int Delete(int ma)
        {
            string query = "DELETE FROM tbl_Khach_Hang WHERE ma_khach_hang = " + ma;
            return DataProvider.Instance.execNonQuery(query);
        }
    }
}
