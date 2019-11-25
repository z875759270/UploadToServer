using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Daaa
{
    public partial class Frm_main : Form
    {
        public Frm_main()
        {
            InitializeComponent();
        }

        private void 全景录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Vr frm_Vr = new Frm_Vr();
            frm_Vr.Show();
        }

        private void 商品录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Good frm_Good = new Frm_Good();
            frm_Good.Show();
        }

        private void 上传至ossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            upload2oss frm_upload2Oss = new upload2oss();
            frm_upload2Oss.Show();
        }

        private void 上传图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uploadImg2oss uploadImg2Oss = new uploadImg2oss();
            uploadImg2Oss.Show();
        }
    }
}
