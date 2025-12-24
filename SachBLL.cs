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
    public class SachBLL
    {
        SachDAL dal = new SachDAL();

        public DataTable LayDanhSach()
        {
            return dal.GetList();
        }

        public bool ThemSach(SachEntity sach)
        {
            // Có thể thêm kiểm tra nghiệp vụ ở đây (ví dụ: tên không được để trống)
            if (string.IsNullOrEmpty(sach.TenSach)) return false;
            return dal.Insert(sach) > 0;
        }

        public bool SuaSach(SachEntity sach)
        {
            return dal.Update(sach) > 0;
        }

        public bool XoaSach(int ma)
        {
            return dal.Delete(ma) > 0;
        }
    }
}
