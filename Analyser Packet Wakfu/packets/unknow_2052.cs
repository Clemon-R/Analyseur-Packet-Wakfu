using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu.packets
{
    public class unknow_2052 : default_packet
    {
        private long nbr1;
        private bool nbr2;

        public unknow_2052(default_packet pck) : base(pck.Len,pck.ID, pck.linker, pck.type)
        {
            this.know = true;
        }

        public override void  decode(BigEndianReader packet)
        {
            if (linker is Client)
            {
                nbr1 = packet.ReadInt();
                nbr2 = packet.ReadBoolean();
            }
            else
                nbr2 = packet.ReadBoolean();
        }

        public override BigEndianWriter encode()
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteUShort(this.Len);
            if (this.linker is Server)
                packet.WriteByte(this.type);
            packet.WriteUShort(this.ID);
            if (this.linker is Client)
            {
                packet.WriteLong(nbr1);
                packet.WriteBoolean(nbr2);
            }
            else
                packet.WriteBoolean(nbr2);
            return (packet);
        }

        public override void display(ListBox list)
        {
            if (linker is Client)
            {
                list.Items.Add("Unknow: " + nbr1);
                list.Items.Add("Unknow: " + (nbr2 ? "true" : "false"));
            }
            else
            {
                list.Items.Add("Unknow: " + (nbr2 ? "true" : "false"));
            }
        }
    }
}
