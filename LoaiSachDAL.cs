using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLNS
{
    public class LoaiSachDAL
    {
        // 1. Hàm lấy danh sách
        public DataTable GetList()
        {
            return DataProvider.Instance.execQuery("SELECT * FROM tbl_Loai_Sach ");
        }

        // 2. Hàm thêm
        public int Insert(string ten)
        {
            string query = string.Format("EXEC proc_them_loai_sach @tenLoaiSach = N'{0}'", ten);
            return DataProvider.Instance.execNonQuery(query);
        }

        // 3. Hàm sửa
        public int Update(int ma, string ten)
        {
            string query = string.Format("EXEC proc_cap_nhat_loai_sach @maLoaiSach = {0}, @tenLoaiSach = N'{1}'", ma, ten);
            return DataProvider.Instance.execNonQuery(query);
        }

        // 4. Hàm xóa
        public int Delete(int ma)
        {
            string query = "DELETE FROM tbl_Loai_Sach WHERE ma_loai_sach = " + ma;
            return DataProvider.Instance.execNonQuery(query);
        }

        // 5. Hàm lấy mã loại theo tên (Quan trọng để sửa lỗi dòng 27 bên BLL)
        public int GetMaLoaiByName(string ten)
        {
            string query = "SELECT ma_loai_sach FROM tbl_Loai_Sach WHERE ten_loai_sach = N'" + ten + "'";
            object result = DataProvider.Instance.execScaler(query);
            return result != null ? (int)result : 0;
        }
    }
}
