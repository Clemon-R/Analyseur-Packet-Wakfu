using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu.packets
{
    public class unknow_1034 : default_packet
    {
        private int nbr1;
        private byte[] data;

        public unknow_1034(default_packet pck) : base(pck.Len,pck.ID, pck.linker,pck.type)
        {
            this.know = true;
        }

        public override BigEndianWriter encode()
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteUShort(this.Len);
            if (this.linker is Server)
                packet.WriteByte(this.type);
            packet.WriteUShort(this.ID);
            packet.WriteInt(nbr1);
            packet.WriteBytes(data);
            return (packet);
        }

        public override void decode(BigEndianReader packet)
        {
            if (this.linker is Client)
            {
                nbr1 = packet.ReadInt();
                data = packet.ReadBytes(this.Len - (sizeof(ushort) * 2 + sizeof(int)) + 10);
            }
        }

        public override void display(ListBox list)
        {
            list.Items.Add("Unknow: " + nbr1);
            list.Items.Add("Unknow: " + Encoding.UTF8.GetString(data));
        }
    }
}
