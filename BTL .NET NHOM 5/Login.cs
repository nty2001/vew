using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL.NET_NHOM_5
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            label4.Text = "";
        }
        public static string ConnectionString = "Data Source=NTTRANG2\\MSSQLSERVERYEN;Initial Catalog=QLNS_HUMG;Integrated Security=True";
        public static string quyen = "user";
        private void Login_Click(object sender, EventArgs e)
        {
            try
            {
              
                
                //Kiểm tra textBox tài khoản ,mật khẩu đã nhập chưa
                if (textBox1.Text != null || textBox1.Text.Trim() == "")
                {

                }
                else
                {
                    MessageBox.Show("Chưa nhập tên tài khoản", "Thông báo");
                    textBox1.Focus();
                    return;
                }
                if (textBox2.Text != null || textBox2.Text.Trim() == "")
                {

                }
                else
                {
                    MessageBox.Show("Chưa nhập tên mật khẩu", "Thông báo");
                    textBox2.Focus();
                    return;
                }
                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Open();
                }
                string TenTaiKhoan = textBox1.Text.Trim();
                string MatKhau = textBox2.Text.Trim();
                string query = "SELECT * FROM TaiKhoan WHERE TenTaiKhoan='" + TenTaiKhoan + "' AND  MatKhau='" + MatKhau + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    quyen = ds.Tables[0].Rows[0]["Quyen"].ToString();
                    Main main = new Main(quyen);
                    main.Show();
                    this.Hide();
                }
                else
                {
                    label4.Text = "Thông tin tài khoản hoặc mật khẩu nhập chưa chính xác";
                }

            }
            catch(Exception ex)
            {
                label4.Text = ex.Message;
            }



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
            }
            else
            {
                //Hides Textbox password
                textBox2.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            }
        }
    }
}
