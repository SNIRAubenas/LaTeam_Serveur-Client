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
        private TcpClient client;
        private NetworkStream stream;
        private Thread thread;
        private Mutex mutex;
        private Serveur serveur;

        public Client(TcpClient client, Serveur serveur)
        {
            this.client = client;
            this.stream = client.GetStream();
            this.serveur = serveur;
        }

        public void Start()
        {
            this.thread = new Thread(this.threadStart);
            this.thread.Start();
        }

        void threadStart(object? obj)
        {
            byte[] buffer = new byte[2048];
            mutex = new Mutex(true);

            do
            {
                try
                {
                    int read = stream.Read(buffer, 0, buffer.Length);

                    String recu = ASCIIEncoding.ASCII.GetString(buffer);
                    Console.WriteLine(recu);
                    
                    foreach(Client c in serveur.clients)
                    {
                        //mutex.ReleaseMutex();
                        c.stream.Write(buffer, 0, read);
                        //mutex.WaitOne();
                    }


                    //stream.Write(buffer, 0, read);
                }
                catch
                {
                    break;
                }

            } while (true);

        }

        public void Stop() 
        {
            this.client.Close();
        }
    }
}
