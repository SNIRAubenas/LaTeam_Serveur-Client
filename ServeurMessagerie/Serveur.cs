using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace ServeurMessagerie

{
    internal class Serveur
    {
        private TcpListener tcpListener;
        public List<Client> clients = new List<Client>();
        private Mutex mutexClient = new Mutex();

        public Serveur()
        {

            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder();
           
            builder.DataSource = "archives.db";

            SqliteConnection bdd = new SqliteConnection(builder.ConnectionString);
            bdd.Open();

            var command = bdd.CreateCommand();

           

            //command.CommandText = @"CREATE TABLE utilisateurs (
	           //                     user_id INTEGER PRIMARY KEY,
	           //                     username TEXT NOT NULL,
	           //                     password TEXT NOT NULL)";

            //command.ExecuteNonQuery();

            command.CommandText = @"INSERT INTO utilisateurs (username, password) VALUES('lol','lol')";
            command.ExecuteReader();

            this.tcpListener = new TcpListener(IPAddress.Any, 6666);
            this.tcpListener.Start();

            Console.WriteLine("Serveur démarré !");

            while (true)
            {
                TcpClient tcpClient = this.tcpListener.AcceptTcpClient();
                Console.WriteLine("Nouveau client !");

                Client client = new Client(tcpClient, this, bdd);

                mutexClient.WaitOne();// Acquérir le Mutex avant de manipuler la liste des clients
                clients.Add(client);
                mutexClient.ReleaseMutex(); //Lacher le Mutex

                Thread clientThread = new Thread(new ThreadStart(client.Run));//Lancement du client(Thread)
                clientThread.Start();
            }
        }

        public void BroadcastMessage(Client sender, String message)
        {

            mutexClient.WaitOne();// Acquérir le Mutex avant de manipuler la liste des clients
            foreach (var client in clients)
            {
                client.SendMessage(message);
            }
            mutexClient.ReleaseMutex(); //Lacher le Mutex après avoir manipuler la liste des clients
        }

    }
}
