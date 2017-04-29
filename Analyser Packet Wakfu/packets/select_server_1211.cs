using Analyser_Packet_Wakfu.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu.packets
{
    public class select_server_1211 : default_packet
    {
        private int v1;
        private long v2;

        public select_server_1211(default_packet pck) : base(pck.Len,pck.ID, pck.linker,pck.type)
        {
            this.know = true;
        }

        public override BigEndianWriter encode()
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteUShort(0);
            if (this.linker is Server)
                packet.WriteByte(this.type);
            packet.WriteUShort(this.ID);
            packet.WriteInt(v1);
            packet.WriteLong(v2);
            packet.Seek(0, System.IO.SeekOrigin.Begin);
            packet.WriteUShort((ushort)packet.Data.Length);
            return (packet);
        }


        public override void decode(BigEndianReader packet)
        {
            v1 = packet.ReadInt();
            v2 = packet.ReadLong();
            foreach (server serv in main.servers)
            {
                if (serv.server_id == v1)
                {
                    main.ip = serv.proxy_ip;
                    main.port = serv.proxy_ports[1];
                    break;
                }
            }
        }

        public override void display(ListBox list)
        {
            list.Items.Add("Server ID: " + v1);
            list.Items.Add("Unknow: " + v2);
        }
    }
}
