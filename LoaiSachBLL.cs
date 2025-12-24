using DAL_QLNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLNS
{
    public class LoaiSachBLL
    {
        LoaiSachDAL dal = new LoaiSachDAL();

        public DataTable LayDanhSach() => dal.GetList();

        // Hàm Thêm nhận vào tên loại (string) khớp với Form1
        public bool Them(string ten) => dal.Insert(ten) > 0;

        public bool Sua(int ma, string ten) => dal.Update(ma, ten) > 0;

        public bool Xoa(int ma) => dal.Delete(ma) > 0;

        // Hàm này Form1 đang dùng để lấy ID khi chọn ComboBox
        public int LayMaLoai(string ten)
        {
            return dal.GetMaLoaiByName(ten);
        }
    }
}
