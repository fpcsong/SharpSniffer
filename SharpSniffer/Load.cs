using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace SharpSniffer
{
    public partial class Load : Form
    {
        public BackgroundWorker backGroundWorker;
        public Load()
        {
            InitializeComponent();
        }
        private void Load_Load(object sender, EventArgs e)
        {
            this.progressBar.Value = 0;
            this.progressBar.Maximum = 100;
            backGroundWorker = new BackgroundWorker();
            backGroundWorker.WorkerReportsProgress = true;
            backGroundWorker.DoWork += BackGroundWorker_DoWork;
            backGroundWorker.ProgressChanged += BackGroundWorker_ProgressChanged;
            backGroundWorker.RunWorkerCompleted += BackGroundWorker_RunWorkerCompleted;
            backGroundWorker.RunWorkerAsync();
          //  while (backGroundWorker.IsBusy) ;
        }

        private void BackGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void BackGroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
        }

        private void BackGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Common.LoadDevices(backGroundWorker);
        }
    }
}
