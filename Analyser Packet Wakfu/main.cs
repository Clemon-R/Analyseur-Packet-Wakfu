using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SilverSock;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Analyser_Packet_Wakfu.models;

namespace Analyser_Packet_Wakfu
{
    public partial class main : Form
    {
        public static String ip = "52.16.189.225";
        public static int port = 5558;
        public static List<server> servers;
        private SilverServer Server;
        private Client client;
        private Dictionary<String, default_packet> packets;

        public main()
        {
            InitializeComponent();
            packets = new Dictionary<string, default_packet>();
        }

        public Dictionary<String, default_packet> get_packets()
        {
            return (this.packets);
        }

        private void display_packet(default_packet pck)
        {
            this.id.Text = "ID: " + pck.ID.ToString();
            this.len.Text = "Size : " + pck.Len.ToString();
            this.list_pck.Items.Clear();
            pck.display(this.list_pck);
            this.hd.Visible = true;
        }

        private void select_packet(object sender, EventArgs e)
        {
            if (this.logs.Text.IndexOf(':') == -1)
                return;
            string str = this.logs.Text.Substring(this.logs.Text.IndexOf(':') + 2);
            if (this.packets.ContainsKey(str))
                display_packet(this.packets[str]);
            else
                this.hd.Visible = false;
        }

        private void form_close(object sender, FormClosedEventArgs e)
        {
            if (this.client != null)
            {
                if (this.client.get_server() != null)
                {
                    if (this.client.get_server().get_sender() != null)
                        this.client.get_server().get_sender().Close();
                }
                if (this.client.get_client() != null)
                    this.client.get_client().CloseSocket();
            }
            if (this.Server != null)
                this.Server.Close();
            Process.GetCurrentProcess().Kill();
        }

        public void set_client(Client client)
        {
            this.client = client;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = null;
            utils.add_log(this.logs, "Initialisation du sniffer...");
            Debug.WriteLine("Initialisation du sniffer...");
            Server = new SilverServer("0.0.0.0", 443, 8192);
            Server.OnListeningEvent += new SilverEvents.Listening(Server_OnListeningEvent);
            Server.OnAcceptSocketEvent += new SilverEvents.AcceptSocket(Server_OnAcceptSocketEvent);
            Server.OnListeningFailedEvent += new SilverEvents.ListeningFailed(Server_OnListeningFailedEvent);
            Server.WaitConnection();
        }

        private void Server_OnListeningFailedEvent(Exception ex)
        {
            utils.add_log(this.logs, "Connexion échoué: "+ ex.Message);
            Debug.WriteLine("Connexion échoué: " + ex.Message);
        }

        private void Server_OnAcceptSocketEvent(SilverSocket socket)
        {
            if (this.client == null)
            {
                utils.add_log(this.logs, "Nouvelle connexion client...");
                Debug.WriteLine("Nouvelle connexion client...");
                this.client = new Client(socket, this, this.logs);
            }
        }

        private void Server_OnListeningEvent()
        {
            utils.add_log(this.logs, "En attente de connexion...");
            Debug.WriteLine("En attente de connexion...");
        }
    }
}
