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
        private TcpListener tcpListener;
        public List<Client> clients = new List<Client>();
        private Mutex mutexClient = new Mutex();

        public Serveur()
        {

            this.tcpListener = new TcpListener(IPAddress.Any, 6666);
            this.tcpListener.Start();

            Console.WriteLine("Serveur démarré !");

            while (true)
            {
                TcpClient tcpClient = this.tcpListener.AcceptTcpClient();
                Console.WriteLine("Nouveau client !");

                Client client = new Client(tcpClient, this);

                mutexClient.WaitOne();// Acquérir le Mutex avant de manipuler la liste des clients
                clients.Add(client);
                mutexClient.ReleaseMutex(); //Lacher le Mutex

                Thread clientThread = new Thread(new ThreadStart(client.Run));
                clientThread.Start();
            }
        }

        public void BroadcastMessage(string message, Client sender)
        {
            mutexClient.WaitOne();// Acquérir le Mutex avant de manipuler la liste des clients
            foreach (var client in clients)
            {
                if (client != sender)
                {
                    client.SendMessage(message);
                }
            }
            mutexClient.ReleaseMutex(); //Lacher le Mutex après avoir manipuler la liste des clients
        }

    }
}
