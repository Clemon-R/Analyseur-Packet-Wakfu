using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu.packets
{
    public class close_connection_1 : default_packet
    {
        private byte[] data;
        public close_connection_1(default_packet pck) : base(pck.Len,pck.ID, pck.linker,pck.type)
        {
            this.know = true;
        }

        public override void display(ListBox list)
        {
            list.Items.Add("Fermeture de connexion");
        }

        public override void decode(BigEndianReader packet)
        {
            int size = 4;
            if (this.linker is Server)
                size = 5;
            data = packet.ReadBytes(this.Len - size);
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
