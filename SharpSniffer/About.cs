using System.Windows.Forms;

namespace SharpSniffer
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = linkLabel1.Text.ToString() ;
            System.Diagnostics.Process.Start(url);
        }
    }
}
