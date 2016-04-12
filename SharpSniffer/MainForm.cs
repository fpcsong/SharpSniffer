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
using SharpPcap;
using PacketDotNet;
namespace SharpSniffer
{
    public partial class MainForm : Form
    {
        public BackgroundWorker backgroundWorker;
        public MainForm()
        {
            InitializeComponent();
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Load load = new SharpSniffer.Load();
            load.ShowDialog(this);
            foreach(var item in Common.comboBox.Items)
            {
                comboBox.Items.Add(item);
            }
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.WorkerSupportsCancellation = true;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int deviceIndex = comboBox.SelectedIndex;
            ICaptureDevice device = Common.devices[deviceIndex];
            CaptureBackGround(device);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

            }
        }
        private void  CaptureBackGround(ICaptureDevice device)
        {
            device.OnPacketArrival += Device_OnPacketArrival;
            device.Open();
            device.Capture();
        }
        /// <summary>
        /// 收到数据包之后一方面放在缓存队列里，另一方面显示在主界面
        /// 异步方式防止滚动条失效
        /// 未解决问题：花屏
        /// </summary>
        /// <param name="packet"></param>
        public void PushPacket(Packet packet)
        {
            if (Common.packetQueue.Count > Common.queueSize)
            {
                Common.packetQueue.RemoveAt(0);
                dataGridView.Rows.RemoveAt(0);
            }
            Common.packetQueue.Add(packet);
            PacketDetials pd = new PacketDetials(packet);
            if (this.InvokeRequired)
            {
                lock (this)
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                   {
                       dataGridView.Rows.Add(++Common.cnt, packet.GetType(), pd.ipPacket.SourceAddress, pd.ipPacket.DestinationAddress, pd.ethernetPacket.SourceHwAddress, pd.ethernetPacket.DestinationHwAddress);
                       dataGridView.Rows[dataGridView.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromName(packet.Color);
                   }), null, null);
                }
            }
            else
            {
                lock (this)
                {
                    dataGridView.Rows.Add(++Common.cnt, packet.GetType(), pd.ipPacket.SourceAddress, pd.ipPacket.DestinationAddress, pd.ethernetPacket.SourceHwAddress, pd.ethernetPacket.DestinationHwAddress);
                    dataGridView.Rows[dataGridView.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromName(packet.Color);
                }
            }
            //dataGridView.Refresh();
            //this.Refresh();
        }
        private void Device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            PushPacket(packet);
            
        }

        private void Start_Click(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }
    }
}
