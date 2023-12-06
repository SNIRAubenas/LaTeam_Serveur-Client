using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServeurMessagerie
{
    internal class Message
    {
        private List<string> messages = new List<string>();

        public List<string> MessagesGetterSetter { get => messages; set => messages = value; }
    }
}
