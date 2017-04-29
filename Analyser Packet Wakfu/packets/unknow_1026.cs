using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu.packets
{
    public class unknow_1026 : default_packet
    {
        byte[] data;

        public unknow_1026(default_packet pck) : base(pck.Len,pck.ID, pck.linker,pck.type)
        {
            this.know = true;
        }

        public override void decode(BigEndianReader packet)
        {
            data = packet.ReadBytes(this.Len - (sizeof(ushort) * 2 - (this.linker is Server ? sizeof(byte) : 0)));
        }

        public override BigEndianWriter encode()
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteUShort(this.Len);
            if (this.linker is Server)
                packet.WriteByte(this.type);
            packet.WriteUShort(this.ID);
            packet.WriteBytes(data);
            return (packet);
        }


        public override void display(ListBox list)
        {
            foreach (byte b in data)
                list.Items.Add(b.ToString());
        }
    }
}
