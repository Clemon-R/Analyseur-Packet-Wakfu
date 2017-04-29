using Analyser_Packet_Wakfu.models;
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
    public class unknow_1212 : default_packet
    {
        private byte v1;
        private string v2;

        public unknow_1212(default_packet pck) : base(pck.Len,pck.ID, pck.linker,pck.type)
        {
            this.know = true;
        }

        public override BigEndianWriter encode()
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteUShort(0);
            if (this.linker is Server)
                packet.WriteByte(type);
            packet.WriteUShort(this.ID);
            if (this.linker is Client)
                packet.WriteByte(v1);
            byte[] data = Encoding.UTF8.GetBytes(v2);
            packet.WriteInt(data.Length);
            packet.WriteBytes(data);
            packet.Seek(0, SeekOrigin.Begin);
            packet.WriteUShort((ushort)packet.Data.Length);
            return (packet);
        }


        public override void decode(BigEndianReader packet)
        {
            if (this.linker is Client)
            {
                v1 = packet.ReadByte();
            }
            byte[] data = packet.ReadBytes(packet.ReadInt());
            v2 = Encoding.UTF8.GetString(data);
        }

        public override void display(ListBox list)
        {
            if (this.linker is Client)
            {
                list.Items.Add("Unknow: " + v1);
                list.Items.Add("Ticket de connexion: " + v2);
            }
        }
    }
}
