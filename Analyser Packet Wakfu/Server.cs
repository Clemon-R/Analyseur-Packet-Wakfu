using SilverSock;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu
{
    public class Server
    {
        private ListBox logs;
        private Socket sender;
        private Client client;
        private main main;

        public Server(string ip, int port, ListBox logs, Client client, main main)
        {
            this.client = client;
            this.logs = logs;
            this.main = main;
            utils.add_log(this.logs, "Initialisation du serveur...");
            Debug.WriteLine("Initialisation du serveur...");
            try
            {
                sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                utils.add_log(this.logs, "En attente de connexion...");
                Debug.WriteLine("En attente de connexion...");
            }
            catch (Exception ex)
            {
                utils.add_log(this.logs, "Connexion échoué: " + ex.Message);
                Debug.WriteLine("Connexion échoué: " + ex.Message);
                sender = null;
            }
        }

        public Socket get_sender()
        {
            return (this.sender);
        }

        public void send(byte[] data, default_packet pck)
        {
            string str = utils.byte_to_string(data);
            this.sender.Send(data, 0, data.Length, 0);
            utils.add_log(this.logs, ">-["+pck.ID+"]Packet Server: " + str);
            Debug.WriteLine(">-[" + pck.ID + "]Packet Server: " + str);
        }

        public void receive_packet()
        {
            byte[] bytes;
            string data;

            while (this.sender != null)
            {
                int size;
                bytes = new byte[8192];
                try
                {
                    size = this.sender.Receive(bytes);
                }
                catch { break; }
                Array.Resize(ref bytes, size);
                data = utils.byte_to_string(bytes);
                if (data.Length > 0)
                {
                    default_packet pck = packet_handler.handle_packet(bytes, client);
                    utils.add_log(this.logs, "<-["+pck.ID+"]Packet Server: " + data);
                    Debug.WriteLine("<-[" + pck.ID + "]Packet Server: " + data);
                    Thread.Sleep(25);
                    pck.relink();
                    if (!this.main.get_packets().ContainsKey(data))
                        this.main.get_packets().Add(data, pck);
                    data = null;
                }
            }
        }
    }
}
