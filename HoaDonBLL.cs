using DAL_QLNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLNS
{
    public class HoaDonBLL
    {
        HoaDonDAL dal = new HoaDonDAL();

        public DataTable LayDanhSach() => dal.GetList();

        // Khớp với lệnh gọi hdBLL.Them(...) trong Form1
        public bool Them(DateTime ngay, string tenKh, string sdt)
        {
            return dal.Insert(ngay, tenKh, sdt) > 0;
        }

        public bool Sua(int ma, DateTime ngay, string tenKh, string sdt)
        {
            return dal.Update(ma, ngay, tenKh, sdt) > 0;
        }

        public bool Xoa(int ma) => dal.Delete(ma) > 0;
    }
}
