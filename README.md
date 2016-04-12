# SharpSniffer
基于sharppcap项目的简易嗅探器

sharppcap项目见https://github.com/chmorgan/sharppcap

不支持上层协议解析，可以导出包文件使用wireshark查看。

支持TCP UDP ARP ICMP IGMP协议

同时源码里还支持PPPoE和PPP协议，用的较少没有做展示。


#主要功能
通用的tcpdump过滤器语法，在设置中可以查看

支持查看数据包文件和将截获的数据包导出

将鼠标停放在单元格上可查看单元格未显示的完整信息

双击单元格新窗口查看详细

双击协议名称新窗口查看完整数据包（不含上层协议解析）
