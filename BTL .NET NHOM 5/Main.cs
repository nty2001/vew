using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL.NET_NHOM_5
{
    public partial class Main : Form
    {
        public Main(string quyen)
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            if(quyen == "admin")
            {
                nhânSựToolStripMenuItem.Visible = false;
            }
            else if (quyen == "nhansu")
            {

            }
            else if (quyen == "stduent")
            {

            }
        }

        private void chứcVụToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Chucvu chucvu = new Chucvu();
            chucvu.MdiParent = this;
            chucvu.Show();
        }
    }
}
