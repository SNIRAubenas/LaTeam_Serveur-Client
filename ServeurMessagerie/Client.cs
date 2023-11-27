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

        public Client(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
        }

        public void Start()
        {
            this.thread = new Thread(this.threadStart);
            this.thread.Start();
        }

        void threadStart(object? obj)
        {
            byte[] buffer = new byte[2048];

            do
            {
                try
                {
                    int read = stream.Read(buffer, 0, buffer.Length);

                    String recu = ASCIIEncoding.ASCII.GetString(buffer);
                    Console.WriteLine(recu);
                    stream.Write(buffer, 0, read);
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
