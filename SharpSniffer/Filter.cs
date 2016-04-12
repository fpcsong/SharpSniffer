using System;
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
            Common.filter = textBox.Text.ToString();
            try
            {
                if (Common.device != null) Common.device.Filter = Common.filter;
            }
            catch (Exception)
            {
                MessageBox.Show("过滤器参数非法");
                return;
            }
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            if (Common.device != null)
            {
                textBox.Text = Common.device.Filter;
            }
        }
    }
}
