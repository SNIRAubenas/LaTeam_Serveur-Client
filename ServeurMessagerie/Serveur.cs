using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServeurMessagerie
{
    internal class Serveur
    {
        private IPEndPoint ep;
        private TcpListener listener;
        private Thread th;
        private List<Client> clients;


        public Serveur(IPAddress ipLocal)
        {
            // Echo est sur le port 6666
            ep = new IPEndPoint(ipLocal, 6666);
            listener = new TcpListener(ep);
          
            clients = new List<Client>();

        }

        public void Start()
        {
            th = new Thread(this.threadStart);
            th.Start();
        }

        void threadStart()
        {
            listener.Start();
            do
            {
                try
                {                 //AcceptTcpClient est bloquant
                    TcpClient client = listener.AcceptTcpClient();
                    // On met ce client dans un Thread qui va renvoyer tout ce qu'il reçoit jusqu'à ce que le client se ferme
                    Client leClient = new Client(client);
                    clients.Add(leClient);
                    leClient.Start();                   
                }
                catch //On CATCH toutes les erreurs
                {              
                    break;
                }
            } while (true);
        }
        public void Stop()
        {
            // On stop le server
            this.listener.Stop();
            // et tous les clients connectés
            foreach (var client in clients)
            {
                client.Stop();
            }
        }

    }
}
