using System;
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

        private void SetQueueSize_Load(object sender, EventArgs e)
        {
            textBox.Text = Common.queueSize.ToString();
        }
    }
}
