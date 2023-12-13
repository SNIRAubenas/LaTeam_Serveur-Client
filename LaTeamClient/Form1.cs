using MaterialSkin;
using MaterialSkin.Controls;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LaTeamClient
{
    public partial class Form1 : MaterialForm
    {
        TcpClient client;
        NetworkStream flux;
        string? id;
        Thread ReceptionThread;
        public Form1()
        {
            InitialisationUI();
            visibiliterPanel(true, false, false);
        }

        //initialise la partie graph avec les couleur etc etc   
        private void InitialisationUI()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey500, Primary.BlueGrey800,
            Primary.BlueGrey400, Accent.Purple700, TextShade.WHITE);
            this.BackColor = Color.RoyalBlue;
            InitializeComponent();
        }
        //a l'appuye du button connexions, si tout les champs sont remplis correctement, etablie la connexionavec le serveur
        private void buttonConnection_Click(object sender, EventArgs e)
        {

            string aIP = this.materialTextBoxIP.Text;
            int port = 0;
            bool error = false;
            this.errorLabel.ForeColor = Color.Red;
            this.errorLabel.Text = null;
            IPAddress? iPAddress = null;

            try
            {
                iPAddress = IPAddress.Parse(aIP);
                port = int.Parse(this.materialTextBoxPort.Text);
                if ((port <= 0) || (port > 65535))
                {
                    error = true;
                }
            }
            catch
            {
                error = true;
            }
            if (!error)
            {
                client = new TcpClient();
                try
                {
                    client.Connect(iPAddress, port);
                    flux = client.GetStream();
                    lancementThread();
                    MessageErrorLabel("Connection établie.", Color.White);
                    visibiliterPanel(false, true, false);
                }
                catch
                {
                    visibiliterPanel(true, false, false);
                    MessageErrorLabel("Connexion impossible....", Color.Red);
                }
            }
            else
            {
                MessageErrorLabel("Erreur de saisie....", Color.Red);
            }
        }    

            //methode qui tourne sur un thread et qui regarde si rien n'ai reçu si quelque choses reçu elle l'affiche 
            private void tReception()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        byte[] recu = new byte[4096];
                        int lu = flux.Read(recu, 0, recu.Length);
                        if (lu == 0)
                            break;
                        String chaineRecue = ASCIIEncoding.ASCII.GetString(recu, 0, lu).Trim();
                        if (chaineRecue.StartsWith("ListeCon:"))
                        {
                            chaineRecue = chaineRecue.Substring(9);

                            string test = null;
                            string[] nomEnLigne = chaineRecue.Split(";");
                            foreach (string s in nomEnLigne)
                            {
                                this.Invoke(() =>
                                {
                                    
                                    test = test + "-" + s + "\r\n";
                                    this.textBoxEnligne.Text = test;
                                });
                            }
                        }
                        else
                        {
                            this.Invoke(() =>
                            {
                                this.textBoxRecu.Text += "\r\n" + chaineRecue;
                                this.textBoxRecu.Select(this.textBoxRecu.Text.Length, 0);
                                this.textBoxRecu.ScrollToCaret();
                            });
                        }
                    }
                    catch (IOException)
                    {

                        this.Invoke(() =>
                        {
                            this.materialButtonEnvoyer.Visible = false;
                            this.textBoxRecu.Text = "Serveur Fermé";

                        });
                        this.Close();
                    }
                }

            }
            //catch quand la deconnexion a etais reussi  
            catch
            {
                try
                {
                    this.Invoke(() =>
                    {
                        MessageErrorLabel("Déconnexion Reussis", Color.Green);
                    });
                }
                //catch l'erreur de quand la fenetre et deja fermer et qu''il essaye d'ecrire sur un composant 
                catch
                {
                }
            }
        }
        //a l'appuye du button login recuper l'id entrez par l'utilisateur  et l'envoie et ensuite affiche la messageri
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            visibiliterPanel(false, false, true);
            id = this.materialTextBoxLogIn.Text;
            labelLigne.Text = id;
            envoie(id);
            this.Text = "Connecte en tant que " + id;
        }





        //quand on appuie sur le button envoyer recuperation du message ecrit dans  la textBoxEnvoie
        private void buttonEnvoyer_Click(object sender, EventArgs e)
        {
            if (this.textBoxEnvoi.Text != string.Empty)
            {
                envoie(this.textBoxEnvoi.Text);
                textBoxEnvoi.Clear();
            }
        }
        //prend le message le transforme en tableau de byte et l'envoie sur le flux 
        private void envoie(String msgenvoie)
        {
            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(msgenvoie);
            flux.Write(buffer, 0, buffer.Length);
        }


        //methode qui excute fermeture a l'appuie du button deconnexion
        private void deconnexion(object sender, EventArgs e)
        {
            fermeture();
        }

        //ferme la connexion eteind le thread en cours et mets annule l'id du clients 
        private void fermeture()
        {
            if (flux != null && client != null)
            {
                id = null;
                flux.Close();
                client.Close();
                visibiliterPanel(true, false, false);
            }
        }

        //visibiliter des panel pouvoir choisir le(s) quel(s) peuvent etre visible
        private void visibiliterPanel(bool connexion, bool login, bool messagerie)
        {
            panelConnexion.Visible = connexion;
            panelLogIn.Visible = login;
            panelMessagerie.Visible = messagerie;

        }
        //pour pouvoir afficher dans le label en bas a droite l'etat de la connexion
        private void MessageErrorLabel(string message, Color color)
        {
            this.errorLabel.ForeColor = color;
            this.errorLabel.Text = message;

        }
        //lance le Thread qui permet de reçevoir les messages
        private void lancementThread()
        {      
            ReceptionThread = new Thread(new ThreadStart(tReception));     
            ReceptionThread.Start();  
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            fermeture();
        }
    }
}