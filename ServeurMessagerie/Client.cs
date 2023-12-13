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
        private string[] commande;
        //Serveur Messagerie


        //BDD
        private SqliteConnection bdd;
        private string username;
        private string id;
        private string password;

        private SqliteCommand sqlSelectUser;
        private SqliteCommand sqlInsertUser;
        private SqliteCommand sqlDeleteUser;

        private SqliteCommand sqlSelectMaxUserId;

        private SqliteCommand sqlInsertMessage;
        private SqliteCommand sqlMaxMessageId;
        private SqliteCommand sqlSelectLastMessages;

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

                    SendSQL(1); //On lance une requete SQLite pour savoir notre utilisateur existe déjâ dans la bdd 

                    ExecuteReaderFromSql(sqlSelectUser); //On execute la commande
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
                        SendSQL(2); //On insert le nouveau utilisateur

                        SendSQL(3); //On recup l'id du nouveau utilisateur

                        ExecuteReaderFromSql(sqlSelectMaxUserId);

                        while (result.Read())
                        {
                            this.id = result.GetString(0);
                        }

                        this.username = clientMessage;
                    }


                    //ENVOI DES 10 DERNIERS MESSAGES
                    string lastMessages = "lastMessages:\r\n";

                    SendSQL(7);
                    ExecuteReaderFromSql(sqlSelectLastMessages);

                    while (result.Read())
                    {

                        lastMessages = lastMessages + result.GetString(0) + ";";
                        lastMessages = lastMessages + result.GetString(1) + ";";
                        lastMessages = lastMessages + result.GetString(2) + "\r\n";
                    }


                    Console.WriteLine(lastMessages);
                    SendMessage(lastMessages);
                    //ENVOI DES 10 DERNIERS MESSAGES


                }
                else //Si ce n'est pas le premier message
                {
                    if(this.username != null)
                    {

                        SendSQL(4); //On sauvegarde le message envoyé par l'utilisateur dans la bdd


                        commande = clientMessage.Split(" ",3); //On split les 3 premiers espaces de la chaine de caractère, cela nous donne un tableau de string
                        

                        switch (commande[0])
                        {
                            case "/w": //Commande pour envoyer  des message privé

                                /*
                                 * Command[0] = La valeur de la commande
                                 * Command[1] = L'utilisateur concerné
                                 * Command[2] = Contenu lié a l'utilisateur concerné
                                 * 
                                 * */
                                if (commande[1] != null && commande[2] !=null) { 
                                    utilisateurConcerne = commande[1];

                                    foreach (Client c in server.clients)
                                    {
                                        if (c.username.Equals(utilisateurConcerne))
                                        {
                                           
                                            c.SendMessage(commande[2]);
                                        }
                                        else
                                        {
                                            SendMessageErreur("L'utilisateur n'existe pas !");
                                        }                                      
                                    }

                                }
                                else
                                {
                                    SendMessageErreur("Saisie invalide !");
                                }                               
                                break;

                            case "/k": //Commande de kick

                                if(this.username == "admin")
                                {
                                    CloseConnexion(); //On close la connection TCP au client

                                }
                                else
                                {
                                    SendMessageErreur("Vous n'avez pas les droits administrateur !");
                                }
                                
                                break;

                            case "/b": //Commande de ban

                                if (this.username == "admin")
                                {
                                    CloseConnexion(); //On close la connection TCP au client

                                    SendSQL(5); //Et on le supprime de la bdd

                                }
                                else
                                {
                                    SendMessageErreur("Vous n'avez pas les droits administrateur !");
                                }

                                break;

                            case "/modifier": //Commande pour modifier un utilisateur

                                if(this.username == "admin")
                                {
                                    //Modifier un utilisateur

                                }
                                break;



                            default: //Si il n'y a pas de commande tapé, on divulgue le message à tout le monde 
                                string messageFinal = DateTime.Now.ToString("hh:mm") + " " + this.username   + " : \r\n" + clientMessage + "\r\n"; // Formatage de l'envoie : Date NomUser : leMessage
                                server.BroadcastMessage(this, messageFinal); //On envoie quel client a envoyé le message et ce que le client à envoyé.
                                break;
                        }
                    } 
                }

                Console.WriteLine("console : " + clientMessage);

            }//Quand on sort de la boucle, on close la connection TCP et on remove le client de notre List de client

            tcpClient.Close();
            this.server.clients.Remove(this);
        }

        public void SendMessage(string message) //Permet l'envoie d'un string à notre client TCP
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            clientStream.Write(data, 0, data.Length);
            clientStream.Flush();
        }

        public void SendMessageErreur(string message) //Permet l'envoie d'un message d'erreur à notre client TCP
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            clientStream.Write(data, 0, data.Length);
            clientStream.Flush();
        }

        public void CloseConnexion() //Permet de fermer la connection TCP à un utilisateur ciblé
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

        public void ExecuteReaderFromSql(SqliteCommand command) //Execute les commandes SQLite reader
        {
            result = command.ExecuteReader();
        }

        public void SendSQL(int index) //Toutes les requetes SQLite
        {
            switch (index)
            {
                case 1: //Savoir si un utilisateur existe
                    sqlSelectUser = bdd.CreateCommand();

                    sqlSelectUser.CommandText = @"SELECT * FROM utilisateurs WHERE username=$username";
                    sqlSelectUser.Parameters.AddWithValue("$username", clientMessage);
                    break;

                case 2: //Insertion d'un utilisateur dans la bdd
                    sqlInsertUser = bdd.CreateCommand();

                    sqlInsertUser.CommandText = @"INSERT INTO utilisateurs (username, password) VALUES($username,'')";
                    sqlInsertUser.Parameters.AddWithValue("$username", clientMessage);
                    sqlInsertUser.ExecuteNonQuery();
                    break;

                case 3://Selection de l'id du dernier utilisateur
                    sqlSelectMaxUserId = bdd.CreateCommand();

                    sqlSelectMaxUserId.CommandText = @"select max(user_id) from utilisateurs";
                    break;

                case 4://Insertion du message écrit par l'utilisateur dans la bdd
                    sqlInsertMessage = bdd.CreateCommand();

                    sqlInsertMessage.CommandText = @"INSERT INTO message (contenu, user_id,date) VALUES($contenu,$user_id,$date)";
                    sqlInsertMessage.Parameters.AddWithValue("$contenu", clientMessage); //Le contenu du message
                    sqlInsertMessage.Parameters.AddWithValue("$user_id", Int32.Parse(id));//ID du user qui a envoyé le message
                    sqlInsertMessage.Parameters.AddWithValue("$date", DateTime.Now.ToString("hh:mm")); //La date du message envoyé

                    sqlInsertMessage.ExecuteNonQuery();

                    break;

                case 5: //Suppression d'un utilisateur
                    sqlDeleteUser = bdd.CreateCommand();

                    sqlDeleteUser.CommandText = @"DELETE FROM utilisateurs WHERE username=$username";
                    sqlDeleteUser.Parameters.AddWithValue("$username", utilisateurConcerne); //Le nom de l'utilisateur (nom unique)
                    sqlDeleteUser.ExecuteNonQuery();
                    break;

                case 7: //RECUPERATION DES 10 DERNIERS MESSAGES
                    sqlSelectLastMessages = bdd.CreateCommand();

                    sqlSelectLastMessages.CommandText = @"SELECT u.username, m.date, m.contenu FROM utilisateurs u JOIN message m ON u.user_id=m.user_id order BY m.date desc LIMIT 10";
                    break;

            }
        }
    }
}
