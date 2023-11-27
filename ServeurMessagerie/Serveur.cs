using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServeurMessagerie

{
    internal class Serveur
    {

        private IPEndPoint ep;
        private TcpListener listener;
        private Thread th;
        private List<EchoWork> clients;

    }
}
