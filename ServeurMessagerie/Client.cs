using Microsoft.Data.Sqlite;
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
        private string utilisateurConcerne;
        private bool firstMessage = true;
        private Message userMessage;


        //BDD
        private SqliteConnection bdd;
        private string username;
        private int id;
        private string password;
        //BDD

        public Client(TcpClient tcpClient, Serveur server, SqliteConnection bdd)
        {
            this.tcpClient = tcpClient;
            this.server = server;
            this.clientStream = tcpClient.GetStream();
            this.bdd = bdd;
           

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
                    firstMessage = false;

                    var commandeSQL1 = bdd.CreateCommand();

                    commandeSQL1.CommandText = @"SELECT * FROM utilisateurs WHERE username=$username";
                    commandeSQL1.Parameters.AddWithValue("$username", clientMessage);

                    SqliteDataReader r = commandeSQL1.ExecuteReader();
                    string reponse = null;

                    while (r.Read())
                    {
                        reponse = r.GetString(1);
                    }
                    

                    if(reponse != null)
                    {
                        this.username = reponse;
                    } 
                    else
                    {
                        var commandeSQL2 = bdd.CreateCommand();

                        commandeSQL2.CommandText = @"INSERT INTO utilisateurs (username, password) VALUES($username,'')";
                        commandeSQL2.Parameters.AddWithValue("$username", clientMessage);
                        commandeSQL2.ExecuteNonQuery();
                        this.username = clientMessage;
                    }
                    

                } else
                {
                    if(this.username != null)
                    {

                        string[] commande = clientMessage.Split(" ",3);
                        utilisateurConcerne = commande[1];

                        switch (commande[0])
                        {
                            case "/w":
                                if (commande[1] != null && commande[2] !=null) {
                                    

                                    foreach(Client c in server.clients)
                                    {
                                        if (c.username.Equals(utilisateurConcerne))
                                        {
                                           
                                            c.SendMessage(commande[2]);
                                        }                                      
                                    }

                                }                               
                                break;

                            case "/k":

                                if(this.username == "admin")
                                {
                                    foreach (Client c in server.clients)
                                    {
                                        if (c.username.Equals(utilisateurConcerne))
                                        {

                                            c.tcpClient.Close();
                                        }
                                    }

                                }
                                
                                break;

                            case "/b":

                                if (this.username == "admin")
                                {
                                    foreach (Client c in server.clients)
                                    {
                                        if (c.username.Equals(utilisateurConcerne))
                                        {

                                            c.tcpClient.Close();
                                        }
                                    }

                                    var commandeSQL3 = bdd.CreateCommand();

                                    commandeSQL3.CommandText = @"DELETE FROM utilisateurs WHERE username=$username";
                                    commandeSQL3.Parameters.AddWithValue("$username", utilisateurConcerne);
                                    commandeSQL3.ExecuteNonQuery();

                                }

                                break;



                            default:
                                string messageFinal = this.username + " " + DateTime.Now.ToString("hh:mm") + " : \r\n" + clientMessage + "\r\n";
                                server.BroadcastMessage(this, messageFinal);
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
