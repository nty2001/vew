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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BTL.NET_NHOM_5
{
    public partial class HeDaoTao : Form
    {
        public HeDaoTao()
        {
            InitializeComponent();
            SetControl("Reset");
            GetData();
        }
        public static string ConnectionString = "Data Source=NTTRANG2\\MSSQLSERVERYEN;Initial Catalog=QLNS_HUMG;Integrated Security=True";
        public static string quyen = "user";
        public static string State = "-1";
        public void SetControl(string State)
        {
            switch (State)
            {
                case "Reset":
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    label6.Text = "";
                    label8.Text = "";
                    break;
                default:
                    break;
            }
        }
 
        public void GetData()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Open();
                }
                string query = "SELECT * FROM HeDaoTao ORDER By TenHeDaoTao";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                    label6.Text = "Tổng số :" + (dataGridView1.Rows.Count-1) + " bản ghi";

                }
                else
                {
                    dataGridView1.DataSource = null;
                    label6.Text = "Tổng số :0 bản ghi";
                }

            }
            catch (Exception ex)
            {
                label8.Text = ex.Message;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            int index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[index];
            textBox2.Text = row.Cells["IdHeDaoTao"].Value.ToString();
            textBox3.Text = row.Cells["MaHeDaoTao"].Value.ToString();
            textBox4.Text = row.Cells["TenHeDaoTao"].Value.ToString();
            }
              catch (Exception)
            {
                label8.Text = "";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = true;
            button6.Enabled = true;

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            textBox2.Enabled = false;
            textBox3.Focus();
            State = "Insert";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = true;
            button6.Enabled = true;

            textBox2.Enabled = false;
            textBox3.Focus();
            State = "Update";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (State == "Insert")
                {
                    if (textBox3.Text == "") { MessageBox.Show("Chưa nhập thông tin mã hệ đào tạo", "Thông báo"); textBox3.Focus(); return; }
                    if (textBox4.Text == "") { MessageBox.Show("Chưa nhập thông tin tên hệ đào tạo", "Thông báo"); textBox4.Focus(); return; }
                    SqlConnection conn = new SqlConnection(ConnectionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string MaHeDaoTao = textBox3.Text.Trim();
                    string TenHeDaoTao = textBox4.Text.Trim();
                    string query = "INSERT INTO HeDaoTao (MaHeDaoTao,TenHeDaoTao) Values (N'" + MaHeDaoTao + "',N'" + TenHeDaoTao + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công", "Thông báo");
                        GetData();
                    }
                    else{
                        MessageBox.Show("Lỗi dữ liệu", "Thông báo");
                    }

                }
                else if (State == "Update")
                {
                    if (textBox3.Text == "") { MessageBox.Show("Chưa nhập thông tin mã hệ đào tạo", "Thông báo"); textBox3.Focus(); return; }
                    if (textBox4.Text == "") { MessageBox.Show("Chưa nhập thông tin tên hệ đào tạo", "Thông báo"); textBox4.Focus(); return; }
                    SqlConnection conn = new SqlConnection(ConnectionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string IdHeDaoTao = textBox2.Text.Trim();
                    string MaHeDaoTao = textBox3.Text.Trim();
                    string TenHeDaoTao = textBox4.Text.Trim();
                    string query = "UPDATE HeDaoTao SET MaHeDaoTao= N'" + MaHeDaoTao + "',TenHeDaoTao= N'" + TenHeDaoTao + "'  WHERE IdHeDaoTao = '" + IdHeDaoTao + "';";


                    SqlCommand cmd = new SqlCommand(query, conn);
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật dữ liệu thành công", "Thông báo");
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi cập nhật dữ liệu", "Thông báo");
                    }
                }
                SetControl("Reset");
            }
            catch (Exception ex)
            {
                label8.Text = ex.Message;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SetControl("Reset");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult results = MessageBox.Show("Bạn có thực sự muốn xóa???","Xác nhận xóa hệ đào tạo",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (results == DialogResult.Yes)
                {
                    SqlConnection conn = new SqlConnection(ConnectionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string IdHeDaoTao = textBox2.Text.Trim();
                    string query = "DELETE FROM HeDaoTao  WHERE IdHeDaoTao = '" + IdHeDaoTao + "' ";


                    SqlCommand cmd = new SqlCommand(query, conn);
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công", "Thông báo");
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi xóa dữ liệu", "Thông báo");
                    }
                }
              
            }
            catch (Exception ex)
            {
                label8.Text = ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Open();
                }
                string key = textBox1.Text.Trim();
                string query = "SELECT * FROM HeDaoTao WHERE TenHeDaoTao LIKE '%" + key + "%'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                    label6.Text = "Tổng số :" + (dataGridView1.Rows.Count - 1) + " bản ghi";

                }
                else
                {
                    dataGridView1.DataSource = null;
                    label6.Text = "Tổng số :0 bản ghi";
                }

            }
            catch (Exception ex)
            {
                label8.Text = ex.Message;
            }
        }
        Bitmap memoryImage;
        private void button7_Click(object sender, EventArgs e)
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void HeDaoTao_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLNS_HUMGDataSet.HeDaoTao' table. You can move, or remove it, as needed.
            this.HeDaoTaoTableAdapter.Fill(this.qLNS_HUMGDataSet.HeDaoTao);

        }
    }
}
