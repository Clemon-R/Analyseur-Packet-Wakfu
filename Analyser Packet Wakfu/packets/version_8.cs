using SilverSock;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu.packets
{
    public class version_8 : default_packet
    {
        byte version;
        short revision;
        byte change;
        string build;

        public version_8(default_packet pck) : base(pck.Len,pck.ID, pck.linker,pck.type)
        {
            this.know = true;
        }

        public override void decode(BigEndianReader packet)
        {
            if (this.linker is Client)
                packet.ReadByte(); //Useless
            version = packet.ReadByte();
            revision = packet.ReadShort();
            change = packet.ReadByte();
            build = packet.ReadString();
        }

        public override BigEndianWriter encode()
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteUShort(this.Len);
            if (this.linker is Server)
                packet.WriteByte(this.type);
            packet.WriteUShort(this.ID);
            if (this.linker is Client)
                packet.WriteByte(1);
            packet.WriteByte(this.version);
            packet.WriteShort(this.revision);
            packet.WriteByte(this.change);
            packet.WriteString(this.build);
            return (packet);
        }


        public override void display(ListBox list)
        {
            list.Items.Add("Version: " + this.version + "." + this.revision + "." + this.change + "." + this.build);
        }
    }
}
