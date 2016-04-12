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
    public partial class SetQueueSize : Form
    {
        public SetQueueSize()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            int size = 0;
            if (int.TryParse(textBox.Text.ToString(), out size))
            {
                Common.queueSize = size;
                this.Close();
            }
            else
            {
                MessageBox.Show("数字格式错误！");
                return;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
