using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analyser_Packet_Wakfu
{
    public class utils
    {
        public static void add_log(ListBox ctrl, string text)
        {
            try
            {
                if (ctrl.InvokeRequired)
                    ctrl.BeginInvoke((MethodInvoker)delegate () { ctrl.Items.Add(text); });
                else
                    ctrl.Items.Add(text);
            }
            catch
            { };
        }

        public static string byte_to_string(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
