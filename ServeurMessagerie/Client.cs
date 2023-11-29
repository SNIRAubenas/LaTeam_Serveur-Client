﻿using System;
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
        private string username;
        //private Mutex mutex;
        private Serveur serveur;

        public Client(TcpClient client, Serveur serveur)
        {
            mutex = new Mutex();
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
            

            do
            {
                try
                {                                    
                    int read = stream.Read(buffer, 0, buffer.Length);
                    
                    if (read == 0) {
                        break;
                    }

                    String recu = ASCIIEncoding.ASCII.GetString(buffer);
                    string messageFinal = this.username + " : " + recu;

                    Console.WriteLine(messageFinal);

                    byte[] bytesFinal = Encoding.ASCII.GetBytes(messageFinal);

                    foreach (Client c in serveur.clients)
                    {                       
                        c.stream.Write(bytesFinal, 0, bytesFinal.Length);                       
                    }                  
                    mutex.ReleaseMutex();
                    
                }
                catch
                {
                    Console.WriteLine("Client perdu");
                    break;
                }

            } while (true);
            client.Close();
         

        }

        public void Stop() 
        {
            this.client.Close();
        }
    }
}
