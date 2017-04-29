using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu.packets
{
    public class unknow_1027 : default_packet
    {
        private byte v1;
        private bool v2;
        private bool v3;
        private int v4;
        private byte[] v5;
        private int v6;
        private byte[] v7;
        private byte[] data;

        public unknow_1027(default_packet pck) : base(pck.Len,pck.ID, pck.linker,pck.type)
        {
            this.know = true;
        }

        public override void decode(BigEndianReader packet)
        {
            if (this.linker is Client)
            {
                int size = sizeof(byte) + sizeof(bool);
                v1 = packet.ReadByte();
                if ((v2 = packet.ReadBoolean()))
                {
                    size += sizeof(int) + sizeof(bool);
                    v6 = packet.ReadInt();
                    if ((v3 = packet.ReadBoolean()))
                    {
                        v5 = packet.ReadBytes((v4 = packet.ReadInt()));
                        size += v4 + sizeof(int);
                    }
                    v7 = packet.ReadBytes(this.Len - (size + sizeof(ushort) * 2));
                }
            }
        }

        public override BigEndianWriter encode()
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteUShort(this.Len);
            if (this.linker is Server)
                packet.WriteByte(this.type);
            packet.WriteUShort(this.ID);
            packet.WriteByte(v1);
            packet.WriteBoolean(v2);
            if (v2)
            {
                packet.WriteInt(v6);
                packet.WriteBoolean(v3);
                if (v3)
                {
                    packet.WriteInt(v4);
                    packet.WriteBytes(v5);
                }
                packet.WriteBytes(v7);
            }
            return (packet);
        }

        public override void display(ListBox list)
        {
            if (this.linker is Client)
            {
                list.Items.Add("Etape(pas sur): " + v1);
                list.Items.Add("Pays disponible: " + (v2 ? "true" : "false"));
                if (v2)
                {
                    list.Items.Add("Pays: " + (v6 == 0 ? "fr" : v6.ToString()));
                    list.Items.Add("Connecté au serveur: "+ (v3 ? "true" : "false"));
                    if (v3) ///Pasfini
                    {
                        MemoryStream memoryStream = new MemoryStream(v5);
                        BinaryReader binaryReader = new BinaryReader(memoryStream);
                        list.Items.Add("m_Account_ID: " + binaryReader.ReadInt32());
                        int size = binaryReader.ReadInt32();
                        list.Items.Add("Size: " + size);
                        byte[] infos = binaryReader.ReadBytes(size);
                        list.Items.Add("Account Name" + Encoding.UTF8.GetString(infos));
                        int taille = binaryReader.ReadInt32();
                        list.Items.Add("Size: " + taille);
                        for (int i = 0;i < taille;i++)
                        {
                            list.Items.Add("m_Server ID: " + binaryReader.ReadInt32());
                            list.Items.Add("m_Right: " + binaryReader.ReadInt32());
                        }
                    }
                    list.Items.Add("Unknow: " + utils.byte_to_string(v7)); //GeneratedMessageV3.parseWithIOException(jdField_a_of_type_ComGoogleProtobufParser, paramInputStream)
                }
            }
        }
    }
}
