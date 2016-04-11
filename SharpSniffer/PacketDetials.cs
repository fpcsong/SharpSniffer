using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketDotNet;
using SharpPcap;
namespace SharpSniffer
{
    class PacketDetials
    {
        private Packet packet;
        public IpPacket ipPacket;
        public ARPPacket arpPacket;
        public EthernetPacket ethernetPacket;
        public ICMPv4Packet icmpv4Packet;
        public ICMPv6Packet icmpv6Packet;
        public IGMPv2Packet igmpv2Packet;
        public PPPoEPacket pppoePacket;
        public PPPPacket pppPacket;
        public TcpPacket tcpPacket;
        public UdpPacket udpPacket;
        
        public PacketDetials(Packet packet)
        {
            this.packet = packet;
            ipPacket = IpPacket.GetEncapsulated(packet);
            arpPacket = ARPPacket.GetEncapsulated(packet);
            ethernetPacket = EthernetPacket.GetEncapsulated(packet);
            icmpv4Packet = ICMPv4Packet.GetEncapsulated(packet);
            icmpv6Packet = ICMPv6Packet.GetEncapsulated(packet);
            igmpv2Packet = IGMPv2Packet.GetEncapsulated(packet);
            pppoePacket = PPPoEPacket.GetEncapsulated(packet);
            pppPacket = PPPPacket.GetEncapsulated(packet);
            tcpPacket = TcpPacket.GetEncapsulated(packet);
            udpPacket = UdpPacket.GetEncapsulated(packet);
        }
    }
}
