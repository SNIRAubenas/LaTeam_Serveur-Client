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

        private Client client;
        public Serveur()
        {

            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder();


            //Création de la bdd a l'endroit par default : C:\Users\%user\source\repos\LaTeam_Serveuripide\ServeurMessagerie\bin\Debug\net7.0
            builder.DataSource = "archives.db";

            //Connection
            SqliteConnection bdd = new SqliteConnection(builder.ConnectionString);
            bdd.Open();

            //Création d'une command SQLite
            var command = bdd.CreateCommand();

            //Création d'une table utilisateurs
            command.CommandText = @"CREATE TABLE IF NOT EXISTS utilisateurs( user_id INTEGER PRIMARY KEY,username TEXT NOT NULL,
            password TEXT NOT NULL)";
                       
            //On execute la commande
            command.ExecuteNonQuery();

            //Création d'une table message avec comme contrainte le user_id
            command.CommandText = @"CREATE TABLE IF NOT EXISTS message (message_id INTEGER PRIMARY KEY, user_id INTEGER NOT NULL REFERENCES utilisateurs (user_id) ON DELETE CASCADE ON UPDATE CASCADE, contenu TEXT NOT NULL, date TEXT NOT NULL);";

            //Execution de la commande
            command.ExecuteNonQuery();
            
            //Ouvre un TCPListener sur nimporte qu'elle adresse de la machine sur le port 6666
            this.tcpListener = new TcpListener(IPAddress.Any, 6666);

            //On start le listener/Server
            this.tcpListener.Start();

            Console.WriteLine("Serveur démarré !");

            //Boucle
            while (true)
            {
                //Accept la connection
                TcpClient tcpClient = this.tcpListener.AcceptTcpClient();
                Console.WriteLine("Nouveau client !");

                //Création du client
                client = new Client(tcpClient, this, bdd);

                //ENVOIE USER CO

                Thread userListThreah = new Thread(new ThreadStart(sendUserList));
                userListThreah.Start();

                mutexClient.WaitOne();// Acquérir le Mutex avant de manipuler la liste des clients
                clients.Add(client); //On l'ajoute à notre List de Client
                mutexClient.ReleaseMutex(); //Lacher le Mutex

                Thread clientThread = new Thread(new ThreadStart(client.Run));//Lancement du client(Thread)
                clientThread.Start();
            }
        }


        public void sendUserList()
        {
            while(true) 
            {
                string listUser = "ListeCon:";

                foreach (Client client1 in clients)
                {
                    listUser += client1.username + ";";
                }

                BroadcastMessage(client, listUser);
                Thread.Sleep(2000);
            }
            
        }



        //Methode permettant l'envoie d'un message à tout le monde
        public void BroadcastMessage(Client sender, String message)
        {

            mutexClient.WaitOne();// Acquérir le Mutex avant de manipuler la liste des clients
            foreach (var client in clients)
            {
                client.SendMessage(message);// Envoie du message à chaque client de la list
            }
            mutexClient.ReleaseMutex(); //Lacher le Mutex après avoir manipuler la liste des clients
        }

    }
}
