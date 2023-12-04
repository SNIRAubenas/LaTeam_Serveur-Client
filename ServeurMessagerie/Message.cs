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



        public Message(String message)
        {
            messages.Add(message);
        }

        public List<string> getMessages { get => messages; }
    }
}
