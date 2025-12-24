using DAL_QLNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLNS
{
    public class KhachHangBLL
    {
        KhachHangDAL dal = new KhachHangDAL();

        public DataTable LayDanhSach() => dal.GetList();

        public bool Them(string ten, string sdt, string diachi)
        {
            return dal.Insert(ten, sdt, diachi) > 0;
        }

        public bool Sua(int ma, string ten, string sdt, string diachi)
        {
            return dal.Update(ma, ten, sdt, diachi) > 0;
        }

        public bool Xoa(int ma) => dal.Delete(ma) > 0;
    }
}
