using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpPcap;
using SharpPcap.LibPcap;
using PacketDotNet;
namespace SharpSniffer
{
    public partial class CellDetails : Form
    {
        public CellDetails()
        {
            InitializeComponent();
        }
        public RawCapture rawCapture;
        private void 导出为cap文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                CaptureFileWriterDevice device = new CaptureFileWriterDevice(saveFileDialog.FileName);
                device.Write(rawCapture);
                device.Close();
            }
        }
    }
}
