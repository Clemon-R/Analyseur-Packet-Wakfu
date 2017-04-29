using SilverSock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu
{
    public abstract class default_packet
    {
        public ushort Len;
        public ushort ID;
        public object linker;
        public byte type;
        public bool know;

        public default_packet(ushort len, ushort id, object linker, byte type)
        {
            this.ID = id;
            this.Len = len;
            this.linker = linker;
            this.type = type;
            this.know = false;
        }
        public abstract void decode(BigEndianReader packet);
        public abstract BigEndianWriter encode();
        public abstract void display(ListBox list);
        public void relink()
        {
            if (linker is Client)
            {
                (this.linker as Client).send(this.encode().Data, this);
            }
            else if (linker is Server)
            {
                (this.linker as Server).send(this.encode().Data, this);
            }
        }
    }
}
