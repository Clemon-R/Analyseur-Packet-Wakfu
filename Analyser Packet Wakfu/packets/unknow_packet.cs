using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu.packets
{
    public class unknow_packet : default_packet
    {
        private byte[] data;
        public unknow_packet(ushort len, ushort id, object linker, byte type) : base(len,id,linker,type)
        { }

        public override void display(ListBox list)
        {
        }

        public override void decode(BigEndianReader packet)
        {
            data = packet.ReadBytes((int)packet.BytesAvailable);
        }

        public override BigEndianWriter encode()
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteUShort(this.Len);
            if (this.linker is Server)
                packet.WriteByte(this.type);
            packet.WriteUShort(this.ID);
            packet.WriteBytes(this.data);
            return (packet);
        }
    }
}
