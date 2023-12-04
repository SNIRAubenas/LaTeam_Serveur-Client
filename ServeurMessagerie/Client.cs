using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServeurMessagerie
{
    class Client
    {
        private TcpClient tcpClient;
        private Serveur server;
        private NetworkStream clientStream;
        private string username;
        private bool firstMessage = true;

        public Client(TcpClient tcpClient, Serveur server)
        {
            this.tcpClient = tcpClient;
            this.server = server;
            this.clientStream = tcpClient.GetStream();
        }

        public void Run()
        {
            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                    break;

                string clientMessage = Encoding.ASCII.GetString(message, 0, bytesRead);

                if (firstMessage)
                {
                    this.username = clientMessage;
                    firstMessage = false;
                } else
                {
                    if(this.username != null)
                    {
                        server.BroadcastMessage(this.username + " : " + clientMessage, this);
                    } 
                }

                Console.WriteLine(clientMessage);

            }

            tcpClient.Close();
            this.server.clients.Remove(this);
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            clientStream.Write(data, 0, data.Length);
            clientStream.Flush();
        }
    }
}
