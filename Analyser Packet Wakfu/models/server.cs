using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyser_Packet_Wakfu.models
{
    public class server
    {
        public int proxy_id;
        public string proxy_name;
        public int country;
        public string proxy_ip;
        public List<int> proxy_ports;
        public byte position;
        public int server_id;
        public byte[] version;
        public Dictionary<short, string> properties;
        public bool locked;
    }
}
