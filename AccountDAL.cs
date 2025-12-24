using Entity_QLNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLNS
{
    public class AccountDAL
    {
        public bool CheckLogin(AccountEntity acc)
        {
            // Truy vấn kiểm tra sự tồn tại của tài khoản
            string query = string.Format("SELECT COUNT(*) FROM tbl_Account WHERE username = '{0}' AND password = '{1}'",
                                          acc.TenDangNhap, acc.MatKhau);

            // Sử dụng execScaler vì kết quả trả về là một giá trị (số lượng bản ghi)
            object result = DataProvider.Instance.execScaler(query);
            return (int)result > 0;
        }
    }
}
