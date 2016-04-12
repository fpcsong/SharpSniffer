using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpSniffer
{
    public partial class Filter : Form
    {
        public Filter()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                Common.device.Filter = textBox.Text.ToString();
                this.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("参数非法！");
                return;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            textBox.Text = Common.device.Filter;
        }
    }
}
