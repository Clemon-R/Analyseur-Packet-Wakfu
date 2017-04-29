using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu.packets
{
    public class my_ip_110 : default_packet
    {
        private byte[] data;
        public my_ip_110(default_packet pck) : base(pck.Len,pck.ID, pck.linker,pck.type)
        {
            this.know = true;
        }

        public override void decode(BigEndianReader packet)
        {
            data = packet.ReadBytes(4);
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
            if (this.linker is Client)
            {
                string ip = data[0] + "." + data[1] + "." + data[2] + "." + data[3];
                list.Items.Add("Ton IP: "+ ip);
            }
        }
    }
}
