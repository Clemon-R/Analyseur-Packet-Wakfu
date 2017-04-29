using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu.packets
{
    public class refresh_1035 : default_packet
    {

        public refresh_1035(default_packet pck) : base(pck.Len,pck.ID, pck.linker,pck.type)
        {
            this.know = true;
        }

        public override void decode(BigEndianReader packet)
        {
        }

        public override BigEndianWriter encode()
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteUShort(this.Len);
            if (this.linker is Server)
                packet.WriteByte(this.type);
            packet.WriteUShort(this.ID);
            return (packet);
        }


        public override void display(ListBox list)
        {
            list.Items.Add("Demande de refresh");
        }
    }
}
