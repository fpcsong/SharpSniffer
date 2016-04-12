using System;
using System.ComponentModel;
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
            DoubleBuffered = true;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int deviceIndex = comboBox.SelectedIndex;
            ICaptureDevice device = Common.devices[deviceIndex];
            Common.device = device;
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
        /// 收到数据包之后判断包类型
        /// 一方面放在缓存队列里，另一方面显示在主界面
        /// 异步方式防止滚动条失效
        /// </summary>
        /// <param name="packet"></param>
        public void PushPacket(Packet packet)
        {
            object[] para = new object[6];
            if (Common.packetQueue.Count > Common.queueSize)
            {
                lock(Common.packetQueue)
                {
                    Common.packetQueue.RemoveRange(0,100);
                }
                if (this.InvokeRequired)
                {
                    lock(this)
                    {
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            for (int i = 0; i < 100;i++) dataGridView.Rows.RemoveAt(0);
                        }));
                    }
                }
                //dataGridView.Rows.RemoveAt(0);
            }
            lock(Common.packetQueue)
            {
                Common.packetQueue.Add(packet);
            }
            PacketDetials pd = new PacketDetials(packet);
            //DataGridViewRow dr = new DataGridViewRow();
            if (pd.typeName == null) return;
            Interlocked.Increment(ref Common.cnt);
            para[0] = Common.cnt;
            para[1] = pd.typeName;
            if (pd.typeName == "TCP" || pd.typeName == "UDP")
            {
                para[2] = pd.ipPacket.SourceAddress;
                para[3] = pd.ipPacket.DestinationAddress;
                para[4] = pd.ethernetPacket.SourceHwAddress;
                para[5] = pd.ethernetPacket.DestinationHwAddress;
            }
            else if (pd.typeName== "ARP")
            {
                para[2] = pd.arpPacket.SenderProtocolAddress;
                para[3] = pd.arpPacket.TargetProtocolAddress;
                para[4] = pd.arpPacket.SenderHardwareAddress;
                para[5] = pd.arpPacket.TargetHardwareAddress;
            }
            else if (pd.typeName == "ICMPv4")
            {
                //para[2] = para[3] = para[4] = para[5] = null;
                para[2] = pd.icmpv4Packet.BytesHighPerformance;
                para[3] = pd.icmpv4Packet.PrintHex();
                para[3] = pd.icmpv4Packet.ToString(StringOutputType.Normal);
            }
            else if (pd.typeName == "IGMPv2")
            {
                para[2] = pd.igmpv2Packet.BytesHighPerformance;
                para[3] = pd.igmpv2Packet.PrintHex();
                para[4] = pd.igmpv2Packet.ToString(StringOutputType.Normal);
            }
            if (this.InvokeRequired)
            {
                lock (this)
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                   {
                       dataGridView.Rows.Add(para);
                       }), null, null);
                }
            }
            else
            {
                dataGridView.Rows.Add(para);
            }
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

        private void Stop_Click(object sender, EventArgs e)
        {
            backgroundWorker.Dispose();
            Common.devices[comboBox.SelectedIndex].Close();
        }

        private void 缓存队列大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetQueueSize setQueueSize = new SetQueueSize();
            setQueueSize.ShowDialog(this);
        }

        private void 过滤器参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            filter.ShowDialog(this);
        }
    }
}
