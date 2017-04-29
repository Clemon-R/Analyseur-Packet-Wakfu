using Analyser_Packet_Wakfu.packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu
{
    public class packet_handler
    {
        public static ListBox logs;
        public static default_packet handle_packet(byte[] packet, object linker)
        {
            ushort len, id;
            byte type;
            MemoryStream memoryStream = new MemoryStream(packet);
            BigEndianReader binaryReader = new BigEndianReader(memoryStream);

            if (linker is Client)
            {
                len = (ushort)(binaryReader.ReadUShort() & 0xFFFF);
                id = binaryReader.ReadUShort();
                type = 0;
            }
            else
            {
                len = binaryReader.ReadUShort();
                type = binaryReader.ReadByte();
                id = binaryReader.ReadUShort();
            }
            default_packet pck = new unknow_packet(len, id, linker, type);
            switch (pck.ID)
            {
                case 1:
                    pck = new close_connection_1(pck);
                    break;

                case 8: //From Seerver
                case 7: //From Client
                    pck = new version_8(pck);
                    break;
                case 110:
                    pck = new my_ip_110(pck);
                    break;
                case 2052:
                    pck = new unknow_2052(pck);
                    break;
                case 1034:
                    pck = new unknow_1034(pck);
                    break;
                case 1026:
                    pck = new unknow_1026(pck);
                    break;
                case 1027:
                    pck = new unknow_1027(pck);
                    break;
                case 1036:
                    pck = new refresh_server_1036(pck);
                    break;
                case 1211:
                    pck = new select_server_1211(pck);
                    break;
                /*case 1213:
                case 1212:
                    pck = new unknow_1212(pck);
                    break;*/
                case 1033:
                    pck = new open_connection_1033(pck);
                    break;
                case 1035:
                    pck = new refresh_1035(pck);
                    break;
                default:
                    utils.add_log(logs, "Unknow Packet: " + pck.ID);
                    Debug.WriteLine("Unknow Packet: " + pck.ID);
                    break;
            }
            pck.decode(binaryReader);
            return pck;
        }
    }
}
