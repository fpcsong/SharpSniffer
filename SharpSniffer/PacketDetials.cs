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
        public string typeName;
        public PacketDetials(Packet packet)
        {
            this.packet = packet;
            ethernetPacket = EthernetPacket.GetEncapsulated(packet);
            if (ethernetPacket != null) typeName = "Ethernet";
            ipPacket = IpPacket.GetEncapsulated(packet);
            if (ipPacket != null) typeName = "Ip";
            arpPacket = ARPPacket.GetEncapsulated(packet);
            if (arpPacket != null) typeName = "ARP";
            icmpv4Packet = ICMPv4Packet.GetEncapsulated(packet);
            if (icmpv4Packet != null) typeName = "ICMPv4";
            icmpv6Packet = ICMPv6Packet.GetEncapsulated(packet);
            if (icmpv6Packet != null) typeName = "ICMPv6";
            igmpv2Packet = IGMPv2Packet.GetEncapsulated(packet);
            if (igmpv2Packet != null) typeName = "IGMPv2";
            pppoePacket = PPPoEPacket.GetEncapsulated(packet);
            if (pppoePacket != null) typeName = "PPPoE";
            pppPacket = PPPPacket.GetEncapsulated(packet);
            if (pppPacket != null) typeName = "PPP";
            tcpPacket = TcpPacket.GetEncapsulated(packet);
            if (tcpPacket != null) typeName = "TCP";
            udpPacket = UdpPacket.GetEncapsulated(packet);
            if (udpPacket != null) typeName = "UDP";
        }
    }
}
