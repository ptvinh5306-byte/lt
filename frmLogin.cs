using System;
using System.Windows.Forms;
using BLL_QLNS; // Bắt buộc phải có dòng này

namespace QLNS
{
    public partial class frmLogin : Form
    {
        private AccountBLL accBLL = new AccountBLL();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text;
            string pass = txtPassword.Text;

            if (accBLL.KiemTraDangNhap(user, pass))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");

                // Mở Form1 (Giao diện chính)
                this.Hide();
                Form1 mainForm = new Form1();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi");
            }
        }
    }
}
