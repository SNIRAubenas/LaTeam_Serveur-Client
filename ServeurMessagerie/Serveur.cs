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
        private Message userMessage = new Message();

        public Serveur()
        {

            this.tcpListener = new TcpListener(IPAddress.Any, 6666);
            this.tcpListener.Start();

            Console.WriteLine("Serveur démarré !");

            while (true)
            {
                TcpClient tcpClient = this.tcpListener.AcceptTcpClient();
                Console.WriteLine("Nouveau client !");

                Client client = new Client(tcpClient, this, userMessage);

                mutexClient.WaitOne();// Acquérir le Mutex avant de manipuler la liste des clients
                clients.Add(client);
                mutexClient.ReleaseMutex(); //Lacher le Mutex

                Thread clientThread = new Thread(new ThreadStart(client.Run));//Lancement du client(Thread)
                clientThread.Start();
            }
        }

        public void BroadcastMessage(Client sender, Message messages)
        {
            string messageAenvoyer = "";

            messages.MessagesGetterSetter.ForEach(m => { 
                if(messageAenvoyer != "")
                {
                    messageAenvoyer = messageAenvoyer + m.ToString().TrimEnd() + "\r\n";
                } else
                {
                    messageAenvoyer =  m.ToString().TrimEnd() + "\r\n";
                }
            });

            mutexClient.WaitOne();// Acquérir le Mutex avant de manipuler la liste des clients
            foreach (var client in clients)
            {
                client.SendMessage(messageAenvoyer);
            }
            mutexClient.ReleaseMutex(); //Lacher le Mutex après avoir manipuler la liste des clients
        }

    }
}
