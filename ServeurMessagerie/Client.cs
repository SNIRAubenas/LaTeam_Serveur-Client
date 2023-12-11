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
        //Serveur Messagerie
        private TcpClient tcpClient;
        private Serveur server;
        private NetworkStream clientStream;
        private string utilisateurConcerne;
        private bool firstMessage = true;
        private string clientMessage;
        //Serveur Messagerie


        //BDD
        private SqliteConnection bdd;
        private string username;
        private string id;
        private string password;


        private SqliteCommand sqlSelectUser;
        private SqliteCommand sqlInsertUser;
        private SqliteCommand sqlInsertMessage;
        private SqliteCommand sqlDeleteUser;

        private SqliteDataReader result;
        //BDD

        //Constructeur du Client
        public Client(TcpClient tcpClient, Serveur server, SqliteConnection bdd)
        {
            this.tcpClient = tcpClient;
            this.server = server;
            this.clientStream = tcpClient.GetStream();
            this.bdd = bdd;
           

        }

        //Methode appelé lors de la création d'un client/Nouvelle connection
        public void Run()
        {
            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096); //Methode .read bloquante, on attends un message
                }
                catch //On catch toute les possibilités
                {
                    break;
                }

                if (bytesRead == 0) //Si il n'y a aucun contenu dans le message lu, on sort de la boucle
                    break;

                //On transforme notre tab de byte en string
                clientMessage = Encoding.ASCII.GetString(message, 0, bytesRead);

                //Si c'est le premier message, cela veut dire que le client nous envoies le nom d'utilisateur
                if (firstMessage)
                {
                    firstMessage = false;

                    SendSQL(1);
                    //Execution d'une commande SQLite
                    result = sqlSelectUser.ExecuteReader();
                    string reponse = null;
              

                    while (result.Read()) //Tant qu'il ya des lignes à lire dans la bdd :
                    {
                        reponse = result.GetString(1); //GetString prend la colone du nom d'utilisateur
                        this.id = result.GetString(0); //GetString prend la colone de l'id de l'utilisateur
                    }
                    

                    if(reponse != null)//Si il y a une reponse
                    {
                        this.username = reponse; //Le username est egale à la reponse
                    } 
                    else //Sinon ça veut dire que c'est un nouveau utilisateur
                    {
                        SendSQL(2);
                        //Execution d'une commande SQLite
                        var commandeSQL5 = bdd.CreateCommand();

                        commandeSQL5.CommandText = @"select max(user_id) from utilisateurs";

                        result = commandeSQL5.ExecuteReader();

                        while (result.Read())
                        {
                            this.id = result.GetString(0);
                        }
                        Console.WriteLine(this.id);
                        this.username = clientMessage;
                    }
                    

                } else //Si ce n'est pas le premier message
                {
                    if(this.username != null)
                    {

                        SendSQL(4);
                        //Execution d'une commande SQLite
                        sqlInsertMessage.ExecuteNonQuery();

                        string[] commande = clientMessage.Split(" ",3);
                        

                        switch (commande[0])
                        {
                            case "/w":
                                if (commande[1] != null && commande[2] !=null) {
                                    utilisateurConcerne = commande[1];

                                    foreach (Client c in server.clients)
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
                                    utilisateurConcerne = commande[1];
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
                                    utilisateurConcerne = commande[1];
                                    foreach (Client c in server.clients)
                                    {
                                        if (c.username.Equals(utilisateurConcerne))
                                        {

                                            c.tcpClient.Close();
                                        }
                                    }

                                    SendSQL(5);

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

        public void SendSQL(int index)
        {
            switch (index)
            {
                case 1:
                    sqlSelectUser = bdd.CreateCommand();

                    sqlSelectUser.CommandText = @"SELECT * FROM utilisateurs WHERE username=$username";
                    sqlSelectUser.Parameters.AddWithValue("$username", clientMessage);
                    break; 

                case 2:
                    sqlInsertUser = bdd.CreateCommand();

                    sqlInsertUser.CommandText = @"INSERT INTO utilisateurs (username, password) VALUES($username,'')";
                    sqlInsertUser.Parameters.AddWithValue("$username", clientMessage);
                    sqlInsertUser.ExecuteNonQuery();
                    break;

                case 3:
                    break; 

                case 4:
                    sqlInsertMessage = bdd.CreateCommand();

                    sqlInsertMessage.CommandText = @"INSERT INTO message (contenu, user_id,date) VALUES($contenu,$user_id,$date)";
                    sqlInsertMessage.Parameters.AddWithValue("$contenu", clientMessage);
                    sqlInsertMessage.Parameters.AddWithValue("$user_id", Int32.Parse(id));
                    sqlInsertMessage.Parameters.AddWithValue("$date", DateTime.Now.ToString("hh:mm"));

                    sqlInsertMessage.ExecuteNonQuery();
                    break;

                case 5:
                    sqlDeleteUser = bdd.CreateCommand();

                    sqlDeleteUser.CommandText = @"DELETE FROM utilisateurs WHERE username=$username";
                    sqlDeleteUser.Parameters.AddWithValue("$username", utilisateurConcerne);
                    sqlDeleteUser.ExecuteNonQuery();
                    break;
            }
        }
    }
}
