namespace LaTeamClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBoxEnvoi = new TextBox();
            textBoxRecu = new TextBox();
            statusStrip1 = new StatusStrip();
            errorLabel = new ToolStripStatusLabel();
            errorLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            label3 = new Label();
            panelMessagerie = new Panel();
            textBoxEnligne = new TextBox();
            pictureBox1 = new PictureBox();
            labelLigne = new Label();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            panelLogIn = new Panel();
            materialTextBoxLogIn = new MaterialSkin.Controls.MaterialTextBox();
            materialButtonLogIn = new MaterialSkin.Controls.MaterialButton();
            materialButtonEnvoyer = new MaterialSkin.Controls.MaterialButton();
            panelConnexion = new Panel();
            materialTextBoxIP = new MaterialSkin.Controls.MaterialTextBox();
            materialButton1 = new MaterialSkin.Controls.MaterialButton();
            materialTextBoxPort = new MaterialSkin.Controls.MaterialTextBox();
            label4 = new Label();
            label5 = new Label();
            textBoxInfoUsers = new TextBox();
            textBoxInfoAdmin = new TextBox();
            statusStrip1.SuspendLayout();
            panelMessagerie.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelLogIn.SuspendLayout();
            panelConnexion.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxEnvoi
            // 
            textBoxEnvoi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxEnvoi.Location = new Point(25, 256);
            textBoxEnvoi.Multiline = true;
            textBoxEnvoi.Name = "textBoxEnvoi";
            textBoxEnvoi.ScrollBars = ScrollBars.Both;
            textBoxEnvoi.Size = new Size(235, 258);
            textBoxEnvoi.TabIndex = 5;
            // 
            // textBoxRecu
            // 
            textBoxRecu.Location = new Point(25, 16);
            textBoxRecu.Multiline = true;
            textBoxRecu.Name = "textBoxRecu";
            textBoxRecu.ReadOnly = true;
            textBoxRecu.ScrollBars = ScrollBars.Both;
            textBoxRecu.Size = new Size(328, 234);
            textBoxRecu.TabIndex = 6;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { errorLabel, errorLabel1, toolStripStatusLabel1 });
            statusStrip1.Location = new Point(3, 618);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1022, 22);
            statusStrip1.TabIndex = 9;
            // 
            // errorLabel
            // 
            errorLabel.ActiveLinkColor = Color.Red;
            errorLabel.Name = "errorLabel";
            errorLabel.Size = new Size(0, 17);
            // 
            // errorLabel1
            // 
            errorLabel1.Name = "errorLabel1";
            errorLabel1.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 39);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 13;
            label3.Text = "Log In = ";
            // 
            // panelMessagerie
            // 
            panelMessagerie.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panelMessagerie.Controls.Add(textBoxEnligne);
            panelMessagerie.Controls.Add(pictureBox1);
            panelMessagerie.Controls.Add(labelLigne);
            panelMessagerie.Controls.Add(materialLabel1);
            panelMessagerie.Controls.Add(textBoxEnvoi);
            panelMessagerie.Controls.Add(materialButtonEnvoyer);
            panelMessagerie.Controls.Add(textBoxRecu);
            panelMessagerie.Location = new Point(17, 83);
            panelMessagerie.Name = "panelMessagerie";
            panelMessagerie.Size = new Size(562, 517);
            panelMessagerie.TabIndex = 15;
            panelMessagerie.Visible = false;
            // 
            // textBoxEnligne
            // 
            textBoxEnligne.Location = new Point(398, 76);
            textBoxEnligne.Multiline = true;
            textBoxEnligne.Name = "textBoxEnligne";
            textBoxEnligne.ReadOnly = true;
            textBoxEnligne.ScrollBars = ScrollBars.Vertical;
            textBoxEnligne.Size = new Size(131, 272);
            textBoxEnligne.TabIndex = 26;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.deco;
            pictureBox1.Location = new Point(505, 441);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(54, 47);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 25;
            pictureBox1.TabStop = false;
            pictureBox1.Click += deconnexion;
            // 
            // labelLigne
            // 
            labelLigne.AutoSize = true;
            labelLigne.Location = new Point(398, 84);
            labelLigne.Name = "labelLigne";
            labelLigne.Size = new Size(0, 15);
            labelLigne.TabIndex = 23;
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(422, 47);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(66, 19);
            materialLabel1.TabIndex = 22;
            materialLabel1.Text = "En Ligne:";
            // 
            // panelLogIn
            // 
            panelLogIn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelLogIn.Controls.Add(materialTextBoxLogIn);
            panelLogIn.Controls.Add(materialButtonLogIn);
            panelLogIn.Controls.Add(label3);
            panelLogIn.Location = new Point(376, 67);
            panelLogIn.Name = "panelLogIn";
            panelLogIn.Size = new Size(226, 79);
            panelLogIn.TabIndex = 16;
            panelLogIn.Visible = false;
            // 
            // materialTextBoxLogIn
            // 
            materialTextBoxLogIn.AnimateReadOnly = false;
            materialTextBoxLogIn.BorderStyle = BorderStyle.None;
            materialTextBoxLogIn.Depth = 0;
            materialTextBoxLogIn.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialTextBoxLogIn.LeadingIcon = null;
            materialTextBoxLogIn.Location = new Point(53, 25);
            materialTextBoxLogIn.MaxLength = 50;
            materialTextBoxLogIn.MouseState = MaterialSkin.MouseState.OUT;
            materialTextBoxLogIn.Multiline = false;
            materialTextBoxLogIn.Name = "materialTextBoxLogIn";
            materialTextBoxLogIn.Size = new Size(88, 50);
            materialTextBoxLogIn.TabIndex = 21;
            materialTextBoxLogIn.Text = "";
            materialTextBoxLogIn.TrailingIcon = null;
            // 
            // materialButtonLogIn
            // 
            materialButtonLogIn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButtonLogIn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButtonLogIn.Depth = 0;
            materialButtonLogIn.HighEmphasis = true;
            materialButtonLogIn.Icon = null;
            materialButtonLogIn.Location = new Point(148, 28);
            materialButtonLogIn.Margin = new Padding(4, 6, 4, 6);
            materialButtonLogIn.MouseState = MaterialSkin.MouseState.HOVER;
            materialButtonLogIn.Name = "materialButtonLogIn";
            materialButtonLogIn.NoAccentTextColor = Color.Empty;
            materialButtonLogIn.Size = new Size(68, 36);
            materialButtonLogIn.TabIndex = 22;
            materialButtonLogIn.Text = "Log In";
            materialButtonLogIn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButtonLogIn.UseAccentColor = false;
            materialButtonLogIn.UseVisualStyleBackColor = true;
            materialButtonLogIn.Click += buttonLogin_Click;
            // 
            // materialButtonEnvoyer
            // 
            materialButtonEnvoyer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButtonEnvoyer.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButtonEnvoyer.Depth = 0;
            materialButtonEnvoyer.HighEmphasis = true;
            materialButtonEnvoyer.Icon = null;
            materialButtonEnvoyer.Location = new Point(267, 259);
            materialButtonEnvoyer.Margin = new Padding(4, 6, 4, 6);
            materialButtonEnvoyer.MouseState = MaterialSkin.MouseState.HOVER;
            materialButtonEnvoyer.Name = "materialButtonEnvoyer";
            materialButtonEnvoyer.NoAccentTextColor = Color.Empty;
            materialButtonEnvoyer.Size = new Size(86, 36);
            materialButtonEnvoyer.TabIndex = 21;
            materialButtonEnvoyer.Text = "Envoyer";
            materialButtonEnvoyer.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButtonEnvoyer.UseAccentColor = false;
            materialButtonEnvoyer.UseVisualStyleBackColor = true;
            materialButtonEnvoyer.Click += buttonEnvoyer_Click;
            // 
            // panelConnexion
            // 
            panelConnexion.Controls.Add(materialTextBoxIP);
            panelConnexion.Controls.Add(materialButton1);
            panelConnexion.Controls.Add(materialTextBoxPort);
            panelConnexion.Controls.Add(label4);
            panelConnexion.Controls.Add(label5);
            panelConnexion.Location = new Point(6, 67);
            panelConnexion.Name = "panelConnexion";
            panelConnexion.Size = new Size(313, 166);
            panelConnexion.TabIndex = 20;
            // 
            // materialTextBoxIP
            // 
            materialTextBoxIP.AnimateReadOnly = false;
            materialTextBoxIP.BorderStyle = BorderStyle.None;
            materialTextBoxIP.Depth = 0;
            materialTextBoxIP.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            materialTextBoxIP.LeadingIcon = null;
            materialTextBoxIP.Location = new Point(95, 22);
            materialTextBoxIP.MaxLength = 50;
            materialTextBoxIP.MouseState = MaterialSkin.MouseState.OUT;
            materialTextBoxIP.Multiline = false;
            materialTextBoxIP.Name = "materialTextBoxIP";
            materialTextBoxIP.Size = new Size(102, 50);
            materialTextBoxIP.TabIndex = 18;
            materialTextBoxIP.Text = "10.0.0.122";
            materialTextBoxIP.TrailingIcon = null;
            // 
            // materialButton1
            // 
            materialButton1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton1.Depth = 0;
            materialButton1.HighEmphasis = true;
            materialButton1.Icon = null;
            materialButton1.Location = new Point(204, 63);
            materialButton1.Margin = new Padding(4, 6, 4, 6);
            materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = Color.Empty;
            materialButton1.Size = new Size(105, 36);
            materialButton1.TabIndex = 17;
            materialButton1.Text = "Connexion";
            materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = true;
            materialButton1.UseVisualStyleBackColor = true;
            materialButton1.Click += buttonConnection_Click;
            // 
            // materialTextBoxPort
            // 
            materialTextBoxPort.AnimateReadOnly = false;
            materialTextBoxPort.BorderStyle = BorderStyle.None;
            materialTextBoxPort.Depth = 0;
            materialTextBoxPort.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            materialTextBoxPort.LeadingIcon = null;
            materialTextBoxPort.Location = new Point(95, 101);
            materialTextBoxPort.MaxLength = 50;
            materialTextBoxPort.MouseState = MaterialSkin.MouseState.OUT;
            materialTextBoxPort.Multiline = false;
            materialTextBoxPort.Name = "materialTextBoxPort";
            materialTextBoxPort.Size = new Size(80, 50);
            materialTextBoxPort.TabIndex = 19;
            materialTextBoxPort.Text = "6666";
            materialTextBoxPort.TrailingIcon = null;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 57);
            label4.Name = "label4";
            label4.Size = new Size(75, 15);
            label4.TabIndex = 0;
            label4.Text = "Adresse IP = ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(46, 139);
            label5.Name = "label5";
            label5.Size = new Size(43, 15);
            label5.TabIndex = 2;
            label5.Text = "Port = ";
            // 
            // textBoxInfoUsers
            // 
            textBoxInfoUsers.Location = new Point(625, 83);
            textBoxInfoUsers.Multiline = true;
            textBoxInfoUsers.Name = "textBoxInfoUsers";
            textBoxInfoUsers.ReadOnly = true;
            textBoxInfoUsers.Size = new Size(376, 212);
            textBoxInfoUsers.TabIndex = 21;
            textBoxInfoUsers.Text = resources.GetString("textBoxInfoUsers.Text");
            // 
            // textBoxInfoAdmin
            // 
            textBoxInfoAdmin.Location = new Point(594, 353);
            textBoxInfoAdmin.Multiline = true;
            textBoxInfoAdmin.Name = "textBoxInfoAdmin";
            textBoxInfoAdmin.ReadOnly = true;
            textBoxInfoAdmin.Size = new Size(428, 247);
            textBoxInfoAdmin.TabIndex = 22;
            textBoxInfoAdmin.Text = resources.GetString("textBoxInfoAdmin.Text");
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.OIP;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1028, 643);
            Controls.Add(textBoxInfoAdmin);
            Controls.Add(textBoxInfoUsers);
            Controls.Add(panelConnexion);
            Controls.Add(statusStrip1);
            Controls.Add(panelLogIn);
            Controls.Add(panelMessagerie);
            Name = "Form1";
            Text = "Messagerie";
            FormClosing += Form1_FormClosing;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panelMessagerie.ResumeLayout(false);
            panelMessagerie.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelLogIn.ResumeLayout(false);
            panelLogIn.PerformLayout();
            panelConnexion.ResumeLayout(false);
            panelConnexion.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxAdresseIP;
        private Label label2;
        private TextBox textBoxPort;
        private Button buttonConnection;
        private TextBox textBoxEnvoi;
        private TextBox textBoxRecu;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel errorLabel;
        private ToolStripStatusLabel errorLabel1;
        private Button buttonLogin;
        private Label label3;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panelMessagerie;
        private Panel panelLogIn;
        private Panel panelConnexion;
        private MaterialSkin.Controls.MaterialButton materialButtonConnection;
        private MaterialSkin.Controls.MaterialTextBox materialTextBoxIP;
        private MaterialSkin.Controls.MaterialTextBox materialTextBoxPort;
        private MaterialSkin.Controls.MaterialTextBox materialTextBoxLogIn;
        private MaterialSkin.Controls.MaterialButton materialButtonLogIn;
        private MaterialSkin.Controls.MaterialButton materialButtonEnvoyer;
        private Label labelLigne;
        private PictureBox pictureBox1;
        private Panel panel1;
        private MaterialSkin.Controls.MaterialTextBox materialTextBox1;
        private MaterialSkin.Controls.MaterialButton materialButton1;
        private MaterialSkin.Controls.MaterialTextBox materialTextBox2;
        private Label label4;
        private Label label5;
        private TextBox textBoxEnligne;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private TextBox textBoxInfoUsers;
        private TextBox textBoxInfoAdmin;
    }
}