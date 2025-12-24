using DAL_QLNS;
using Entity_QLNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLNS
{
    public class AccountBLL
    {
        AccountDAL dal = new AccountDAL();

        public bool KiemTraDangNhap(string user, string pass)
        {
            // Kiểm tra các ràng buộc cơ bản trước khi gọi xuống DAL
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                return false;

            AccountEntity acc = new AccountEntity { TenDangNhap = user, MatKhau = pass };
            return dal.CheckLogin(acc);
        }
    }
}
