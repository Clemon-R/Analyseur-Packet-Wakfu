using Analyser_Packet_Wakfu.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu.packets
{
    public class refresh_server_1036 : default_packet
    {
        private int v1;
        private int v2;
        private List<List<string>> infos;
        private List<List<string>> infos2;

        public refresh_server_1036(default_packet pck) : base(pck.Len, pck.ID, pck.linker, pck.type)
        {
            infos = new List<List<string>>();
            infos2 = new List<List<string>>();
            main.servers = new List<server>();
            this.know = true;
        }

        public override BigEndianWriter encode()
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteUShort(0);
            if (this.linker is Server)
                packet.WriteByte(this.type);
            packet.WriteUShort(this.ID);
            packet.WriteInt(v1);
            for (int i = 0; i < v1; i++)
            {
                server tmp = main.servers[i];
                packet.WriteInt(tmp.proxy_id);
                byte[] args = Encoding.UTF8.GetBytes(tmp.proxy_name);
                packet.WriteInt(args.Length);
                packet.WriteBytes(args);
                packet.WriteInt(tmp.country);
                args = Encoding.UTF8.GetBytes(tmp.server_id == 0 ? "127.0.0.1" : tmp.proxy_ip);
                packet.WriteInt(args.Length);
                packet.WriteBytes(args);
                packet.WriteInt(tmp.proxy_ports.Count);
                for (int p = 0; p < tmp.proxy_ports.Count; p++)
                    packet.WriteInt(tmp.proxy_ports[p]);
                packet.WriteByte(tmp.position);
            }
            packet.WriteInt(v2);
            for (int i = 0; i < v2; i++)
            {
                server tmp = main.servers[i];
                packet.WriteInt(tmp.server_id);
                packet.WriteInt(tmp.version.Length);
                packet.WriteBytes(tmp.version);
                BigEndianWriter writer = new BigEndianWriter();
                writer.WriteInt(tmp.properties.Count);
                foreach (KeyValuePair<short, string> prop in tmp.properties)
                {
                    writer.WriteShort(prop.Key);
                    byte[] args = Encoding.UTF8.GetBytes(prop.Value);
                    writer.WriteInt(args.Length);
                    writer.WriteBytes(args);
                }
                packet.WriteInt(writer.Data.Length);
                packet.WriteBytes(writer.Data);
                packet.WriteBoolean(tmp.locked);
            }
            packet.Seek(0, SeekOrigin.Begin);
            packet.WriteUShort((ushort)packet.Data.Length);
            return (packet);
        }

        public override void decode(BigEndianReader packet)
        {
            if (this.linker is Client)
            {
                v1 = packet.ReadInt();
                for (int i = 0; i < v1; i++)
                {
                    server tmp = new server();
                    tmp.proxy_id = packet.ReadInt();
                    int size = packet.ReadInt();
                    byte[] args = packet.ReadBytes(size);
                    tmp.proxy_name = Encoding.UTF8.GetString(args);
                    tmp.country = packet.ReadInt();
                    args = packet.ReadBytes(packet.ReadInt());
                    tmp.proxy_ip = Encoding.UTF8.GetString(args);
                    size = packet.ReadInt();
                    tmp.proxy_ports = new List<int>();
                    for (int a = 0; a < size; a++)
                    {
                        tmp.proxy_ports.Add(packet.ReadInt());
                    }
                    tmp.position = packet.ReadByte();
                    main.servers.Add(tmp);
                }
                v2 = packet.ReadInt();
                for (int i = 0; i < v2; i++)
                {
                    server tmp = new server();
                    if (main.servers[i] != null)
                        tmp = main.servers[i];
                    tmp.server_id = packet.ReadInt();
                    int size = packet.ReadInt();
                    byte[] args = packet.ReadBytes(size);
                    tmp.version = (byte[])args.Clone();
                    int size1 = packet.ReadInt();
                    byte[] args1 = packet.ReadBytes(size1);
                    MemoryStream memoryStream = new MemoryStream(args1);
                    BigEndianReader binaryReader = new BigEndianReader(memoryStream);
                    int taille = binaryReader.ReadInt();
                    tmp.properties = new Dictionary<short, string>();
                    for (int a = 0; a < taille; a++)
                    {
                        short pid = binaryReader.ReadShort();
                        int s = binaryReader.ReadInt();
                        byte[] str = binaryReader.ReadBytes(s);
                        tmp.properties.Add(pid, Encoding.UTF8.GetString(str));
                    }
                    tmp.locked = packet.ReadBoolean();
                    if (!main.servers.Contains(tmp))
                        main.servers.Add(tmp);
                }
            }
        }

        public override void display(ListBox list)
        {
            if (this.linker is Client)
            {
                list.Items.Add("Nombre de serveur: " + v1.ToString() + " " + v2.ToString());
                foreach (server serv in main.servers)
                {
                    list.Items.Add("Serveur ID: " + serv.server_id);
                    list.Items.Add("Serveur Name: " + serv.proxy_name);
                    list.Items.Add("Locked: " + serv.locked);
                    list.Items.Add("Position: " + serv.position);
                    if (serv.properties != null)
                        foreach (KeyValuePair<short, string> prop in serv.properties)
                            list.Items.Add("Propertie: " + prop.Key.ToString() + " Value: " + prop.Value);
                    string version = "";
                    if (serv.version != null)
                    {
                        foreach (byte b in serv.version)
                            version += version.Length > 0 ? "." + b : b.ToString();
                        list.Items.Add("Serveur Version: " + version);
                    }
                    list.Items.Add("Proxy ID:" + serv.proxy_id);
                    list.Items.Add("Proxy IP: " + serv.proxy_ip);
                    string ports = "";
                    foreach (int port in serv.proxy_ports)
                        ports += ports.Length > 0 ? "," + port : port.ToString();
                    list.Items.Add("Proxy Ports: " + ports);
                }
            }
        }
    }
}
