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
        private string utilisateurConcerne;
        private bool firstMessage = true;
        private Message userMessage;

        public Client(TcpClient tcpClient, Serveur server, Message userMessage)
        {
            this.tcpClient = tcpClient;
            this.server = server;
            this.clientStream = tcpClient.GetStream();
            this.userMessage = userMessage;
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

                        string[] commande = clientMessage.Split(" ",3);
                        

                        switch (commande[0])
                        {
                            case "/w":
                                if (commande[1] != null && commande[2] !=null) {
                                    utilisateurConcerne = commande[1];

                                    foreach(Client c in server.clients)
                                    {
                                        if (c.username.Equals(utilisateurConcerne))
                                        {
                                           
                                            c.SendMessage(commande[2]);
                                        }                                      
                                    }

                                }                               
                                break;

                            default:
                                string messageFinal = this.username + " : " + clientMessage;    
                                userMessage.MessagesGetterSetter.Add(messageFinal);
                                server.BroadcastMessage(this, userMessage);
                                break;
                         

                        }

                                        

                        
                    } 
                }

                Console.WriteLine("console : " + clientMessage);

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
