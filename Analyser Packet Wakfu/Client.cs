using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilverSock;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Analyser_Packet_Wakfu
{
    public class Client
    {
        private SilverSocket socket;
        private main main;
        private ListBox logs;
        private Server server;
        private Thread background;

        public Client(SilverSocket socket, main main, ListBox logs)
        {
            this.socket = socket;
            this.main = main;
            this.logs = logs;
            server = new Server(main.ip, main.port, this.logs, this, this.main);
            background = new Thread(new ThreadStart(server.receive_packet));
            background.Start();
            this.socket.OnDataArrivalEvent += new SilverEvents.DataArrival(_socket_OnDataArrivalEvent);
            this.socket.OnSocketClosedEvent += new SilverEvents.SocketClosed(_socket_OnSocketClosedEvent);
        }

        public SilverSocket get_client()
        {
            return (this.socket);
        }

        public Server get_server()
        {
            return (this.server);
        }

        private void _socket_OnDataArrivalEvent(byte[] data)
        {
            string str = utils.byte_to_string(data);
            default_packet pck = packet_handler.handle_packet(data, this.server);
            utils.add_log(this.logs, "<-[" + pck.ID + "]Packet Client: " + str);
            Debug.WriteLine("<-[" + pck.ID + "]Packet Client: " + str);
            pck.relink();
            if (!this.main.get_packets().ContainsKey(str))
                this.main.get_packets().Add(str, pck);
        }

        private void _socket_OnSocketClosedEvent()
        {
            utils.add_log(this.logs, "Fermeture du client");
            Debug.WriteLine("Fermeture du client");
            this.main.set_client(null);
        }

        public void send(byte[] data, default_packet pck)
        {
            string str = utils.byte_to_string(data);
            this.socket.Send(data);
            utils.add_log(this.logs, ">-[" + pck.ID + "]Packet Client: " + str);
            Debug.WriteLine(">-[" + pck.ID + "]Packet Client: " + str);
            
        }
    }
}
