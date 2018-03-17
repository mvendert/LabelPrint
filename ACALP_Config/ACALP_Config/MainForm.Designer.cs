namespace ACALP_Config
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabInstellingen = new System.Windows.Forms.TabControl();
            this.tabStats = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.chkController = new System.Windows.Forms.CheckBox();
            this.chkDesigner = new System.Windows.Forms.CheckBox();
            this.chkClient = new System.Windows.Forms.CheckBox();
            this.chkServer = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tabAlgemeen = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtLogFolder = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.butSelectFolder = new System.Windows.Forms.Button();
            this.txtDataFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabClient = new System.Windows.Forms.TabPage();
            this.standaloneclientcheck = new System.Windows.Forms.CheckBox();
            this.lblIPServer = new System.Windows.Forms.Label();
            this.lblIPNumberThis = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPortCentralServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPortPrintClient = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkClientFatal = new System.Windows.Forms.CheckBox();
            this.chkClientError = new System.Windows.Forms.CheckBox();
            this.chkClientWarning = new System.Windows.Forms.CheckBox();
            this.chkClientSuccess = new System.Windows.Forms.CheckBox();
            this.butClientPictFolder = new System.Windows.Forms.Button();
            this.chkClientLogAppend = new System.Windows.Forms.CheckBox();
            this.txtClientPictureFolder = new System.Windows.Forms.TextBox();
            this.chkClientInfo = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkClientDebug = new System.Windows.Forms.CheckBox();
            this.butRetrieveName = new System.Windows.Forms.Button();
            this.txtUniqueComputerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.butIPOphalen = new System.Windows.Forms.Button();
            this.txtIPThisComputer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIpNumberServer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabServer = new System.Windows.Forms.TabPage();
            this.chkServerLogAppend = new System.Windows.Forms.CheckBox();
            this.butServerPictureFolder = new System.Windows.Forms.Button();
            this.txtServerPictureFolder = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkServerDebug = new System.Windows.Forms.CheckBox();
            this.chkServerInfo = new System.Windows.Forms.CheckBox();
            this.chkServerSuccess = new System.Windows.Forms.CheckBox();
            this.chkServerWarning = new System.Windows.Forms.CheckBox();
            this.chkServerError = new System.Windows.Forms.CheckBox();
            this.chkServerFatal = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBoxServer = new System.Windows.Forms.PictureBox();
            this.pictureBoxClient = new System.Windows.Forms.PictureBox();
            this.lbClientRunning = new System.Windows.Forms.Label();
            this.lbServerRunning = new System.Windows.Forms.Label();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.lblClientStatus = new System.Windows.Forms.Label();
            this.butRestartAll = new System.Windows.Forms.Button();
            this.butRestartClient = new System.Windows.Forms.Button();
            this.butStartStopServer = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.butRestartServer = new System.Windows.Forms.Button();
            this.butStartStopClient = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.butApply = new System.Windows.Forms.Button();
            this.butOK = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.butResetToInitialValues = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timerServiceStatus = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.imageListImages = new System.Windows.Forms.ImageList(this.components);
            this.tabInstellingen.SuspendLayout();
            this.tabStats.SuspendLayout();
            this.tabAlgemeen.SuspendLayout();
            this.tabClient.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabServer.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClient)).BeginInit();
            this.SuspendLayout();
            // 
            // tabInstellingen
            // 
            this.tabInstellingen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabInstellingen.Controls.Add(this.tabStats);
            this.tabInstellingen.Controls.Add(this.tabAlgemeen);
            this.tabInstellingen.Controls.Add(this.tabClient);
            this.tabInstellingen.Controls.Add(this.tabServer);
            this.tabInstellingen.Controls.Add(this.tabPage1);
            this.tabInstellingen.Location = new System.Drawing.Point(13, 13);
            this.tabInstellingen.Name = "tabInstellingen";
            this.tabInstellingen.SelectedIndex = 0;
            this.tabInstellingen.Size = new System.Drawing.Size(653, 323);
            this.tabInstellingen.TabIndex = 1;
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.label18);
            this.tabStats.Controls.Add(this.chkController);
            this.tabStats.Controls.Add(this.chkDesigner);
            this.tabStats.Controls.Add(this.chkClient);
            this.tabStats.Controls.Add(this.chkServer);
            this.tabStats.Controls.Add(this.label16);
            this.tabStats.Controls.Add(this.label15);
            this.tabStats.Controls.Add(this.label14);
            this.tabStats.Controls.Add(this.label13);
            this.tabStats.Location = new System.Drawing.Point(4, 22);
            this.tabStats.Name = "tabStats";
            this.tabStats.Size = new System.Drawing.Size(645, 297);
            this.tabStats.TabIndex = 4;
            this.tabStats.Text = "Overzicht";
            this.tabStats.UseVisualStyleBackColor = true;
            this.tabStats.Click += new System.EventHandler(this.tabStats_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 13);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(145, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "Geinstalleerde componenten:";
            // 
            // chkController
            // 
            this.chkController.AutoSize = true;
            this.chkController.Enabled = false;
            this.chkController.Location = new System.Drawing.Point(218, 116);
            this.chkController.Name = "chkController";
            this.chkController.Size = new System.Drawing.Size(15, 14);
            this.chkController.TabIndex = 7;
            this.chkController.UseVisualStyleBackColor = true;
            // 
            // chkDesigner
            // 
            this.chkDesigner.AutoSize = true;
            this.chkDesigner.Enabled = false;
            this.chkDesigner.Location = new System.Drawing.Point(218, 62);
            this.chkDesigner.Name = "chkDesigner";
            this.chkDesigner.Size = new System.Drawing.Size(15, 14);
            this.chkDesigner.TabIndex = 6;
            this.chkDesigner.UseVisualStyleBackColor = true;
            // 
            // chkClient
            // 
            this.chkClient.AutoSize = true;
            this.chkClient.Enabled = false;
            this.chkClient.Location = new System.Drawing.Point(218, 89);
            this.chkClient.Name = "chkClient";
            this.chkClient.Size = new System.Drawing.Size(15, 14);
            this.chkClient.TabIndex = 5;
            this.chkClient.UseVisualStyleBackColor = true;
            // 
            // chkServer
            // 
            this.chkServer.AutoSize = true;
            this.chkServer.Enabled = false;
            this.chkServer.Location = new System.Drawing.Point(218, 35);
            this.chkServer.Name = "chkServer";
            this.chkServer.Size = new System.Drawing.Size(15, 14);
            this.chkServer.TabIndex = 4;
            this.chkServer.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(19, 117);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(148, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Beheer programma (controller)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(178, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Label ontwerp programma (designer)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(19, 90);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(127, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Afdruk programma (client)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 36);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(166, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Communicatie programma (server)";
            // 
            // tabAlgemeen
            // 
            this.tabAlgemeen.Controls.Add(this.button3);
            this.tabAlgemeen.Controls.Add(this.button2);
            this.tabAlgemeen.Controls.Add(this.button1);
            this.tabAlgemeen.Controls.Add(this.txtLogFolder);
            this.tabAlgemeen.Controls.Add(this.label17);
            this.tabAlgemeen.Controls.Add(this.butSelectFolder);
            this.tabAlgemeen.Controls.Add(this.txtDataFolder);
            this.tabAlgemeen.Controls.Add(this.label4);
            this.tabAlgemeen.Location = new System.Drawing.Point(4, 22);
            this.tabAlgemeen.Name = "tabAlgemeen";
            this.tabAlgemeen.Padding = new System.Windows.Forms.Padding(3);
            this.tabAlgemeen.Size = new System.Drawing.Size(645, 297);
            this.tabAlgemeen.TabIndex = 0;
            this.tabAlgemeen.Text = "Algemeen";
            this.tabAlgemeen.ToolTipText = "Gegevens zowel voor server als printcomputer";
            this.tabAlgemeen.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(571, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(53, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Maken";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(570, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Maken";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(537, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtLogFolder
            // 
            this.txtLogFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogFolder.Location = new System.Drawing.Point(202, 49);
            this.txtLogFolder.Name = "txtLogFolder";
            this.txtLogFolder.Size = new System.Drawing.Size(329, 20);
            this.txtLogFolder.TabIndex = 3;
            this.txtLogFolder.TextChanged += new System.EventHandler(this.txtLogFolder_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(119, 13);
            this.label17.TabIndex = 13;
            this.label17.Text = "Basismap log gegevens";
            // 
            // butSelectFolder
            // 
            this.butSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSelectFolder.Location = new System.Drawing.Point(538, 18);
            this.butSelectFolder.Name = "butSelectFolder";
            this.butSelectFolder.Size = new System.Drawing.Size(28, 23);
            this.butSelectFolder.TabIndex = 1;
            this.butSelectFolder.Text = "...";
            this.butSelectFolder.UseVisualStyleBackColor = true;
            this.butSelectFolder.Click += new System.EventHandler(this.butSelectFolder_Click);
            // 
            // txtDataFolder
            // 
            this.txtDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataFolder.Location = new System.Drawing.Point(203, 21);
            this.txtDataFolder.Name = "txtDataFolder";
            this.txtDataFolder.Size = new System.Drawing.Size(329, 20);
            this.txtDataFolder.TabIndex = 0;
            this.txtDataFolder.TextChanged += new System.EventHandler(this.txtDataFolder_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Basismap van de gegevens";
            // 
            // tabClient
            // 
            this.tabClient.Controls.Add(this.standaloneclientcheck);
            this.tabClient.Controls.Add(this.lblIPServer);
            this.tabClient.Controls.Add(this.lblIPNumberThis);
            this.tabClient.Controls.Add(this.groupBox1);
            this.tabClient.Controls.Add(this.butRetrieveName);
            this.tabClient.Controls.Add(this.txtUniqueComputerName);
            this.tabClient.Controls.Add(this.label5);
            this.tabClient.Controls.Add(this.butIPOphalen);
            this.tabClient.Controls.Add(this.txtIPThisComputer);
            this.tabClient.Controls.Add(this.label1);
            this.tabClient.Controls.Add(this.txtIpNumberServer);
            this.tabClient.Controls.Add(this.label10);
            this.tabClient.Location = new System.Drawing.Point(4, 22);
            this.tabClient.Name = "tabClient";
            this.tabClient.Padding = new System.Windows.Forms.Padding(3);
            this.tabClient.Size = new System.Drawing.Size(645, 297);
            this.tabClient.TabIndex = 1;
            this.tabClient.Text = "Client";
            this.tabClient.UseVisualStyleBackColor = true;
            // 
            // standaloneclientcheck
            // 
            this.standaloneclientcheck.AutoSize = true;
            this.standaloneclientcheck.Location = new System.Drawing.Point(9, 48);
            this.standaloneclientcheck.Name = "standaloneclientcheck";
            this.standaloneclientcheck.Size = new System.Drawing.Size(109, 17);
            this.standaloneclientcheck.TabIndex = 26;
            this.standaloneclientcheck.Text = "Standalone Client";
            this.standaloneclientcheck.UseVisualStyleBackColor = true;
            this.standaloneclientcheck.CheckedChanged += new System.EventHandler(this.standaloneclientcheck_CheckedChanged);
            // 
            // lblIPServer
            // 
            this.lblIPServer.AutoSize = true;
            this.lblIPServer.Location = new System.Drawing.Point(449, 111);
            this.lblIPServer.Name = "lblIPServer";
            this.lblIPServer.Size = new System.Drawing.Size(16, 13);
            this.lblIPServer.TabIndex = 25;
            this.lblIPServer.Text = "...";
            // 
            // lblIPNumberThis
            // 
            this.lblIPNumberThis.AutoSize = true;
            this.lblIPNumberThis.Location = new System.Drawing.Point(449, 79);
            this.lblIPNumberThis.Name = "lblIPNumberThis";
            this.lblIPNumberThis.Size = new System.Drawing.Size(16, 13);
            this.lblIPNumberThis.TabIndex = 24;
            this.lblIPNumberThis.Text = "...";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPortCentralServer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPortPrintClient);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chkClientFatal);
            this.groupBox1.Controls.Add(this.chkClientError);
            this.groupBox1.Controls.Add(this.chkClientWarning);
            this.groupBox1.Controls.Add(this.chkClientSuccess);
            this.groupBox1.Controls.Add(this.butClientPictFolder);
            this.groupBox1.Controls.Add(this.chkClientLogAppend);
            this.groupBox1.Controls.Add(this.txtClientPictureFolder);
            this.groupBox1.Controls.Add(this.chkClientInfo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.chkClientDebug);
            this.groupBox1.Location = new System.Drawing.Point(9, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(630, 152);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Geavanceerde instellingen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Poortnummer van de server";
            // 
            // txtPortCentralServer
            // 
            this.txtPortCentralServer.Location = new System.Drawing.Point(205, 19);
            this.txtPortCentralServer.Name = "txtPortCentralServer";
            this.txtPortCentralServer.Size = new System.Drawing.Size(100, 20);
            this.txtPortCentralServer.TabIndex = 0;
            this.txtPortCentralServer.TextChanged += new System.EventHandler(this.txtPortCentralServer_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Poortnummer voor controller";
            // 
            // txtPortPrintClient
            // 
            this.txtPortPrintClient.Location = new System.Drawing.Point(205, 46);
            this.txtPortPrintClient.Name = "txtPortPrintClient";
            this.txtPortPrintClient.Size = new System.Drawing.Size(100, 20);
            this.txtPortPrintClient.TabIndex = 1;
            this.txtPortPrintClient.TextChanged += new System.EventHandler(this.txtPortPrintClient_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Niveau foutmeldingen";
            // 
            // chkClientFatal
            // 
            this.chkClientFatal.AutoSize = true;
            this.chkClientFatal.Location = new System.Drawing.Point(129, 76);
            this.chkClientFatal.Name = "chkClientFatal";
            this.chkClientFatal.Size = new System.Drawing.Size(55, 17);
            this.chkClientFatal.TabIndex = 2;
            this.chkClientFatal.Text = "Fataal";
            this.chkClientFatal.UseVisualStyleBackColor = true;
            this.chkClientFatal.CheckedChanged += new System.EventHandler(this.chkClientFatal_CheckedChanged);
            // 
            // chkClientError
            // 
            this.chkClientError.AutoSize = true;
            this.chkClientError.Location = new System.Drawing.Point(191, 76);
            this.chkClientError.Name = "chkClientError";
            this.chkClientError.Size = new System.Drawing.Size(47, 17);
            this.chkClientError.TabIndex = 3;
            this.chkClientError.Text = "Fout";
            this.chkClientError.UseVisualStyleBackColor = true;
            this.chkClientError.CheckedChanged += new System.EventHandler(this.chkClientError_CheckedChanged);
            // 
            // chkClientWarning
            // 
            this.chkClientWarning.AutoSize = true;
            this.chkClientWarning.Location = new System.Drawing.Point(245, 76);
            this.chkClientWarning.Name = "chkClientWarning";
            this.chkClientWarning.Size = new System.Drawing.Size(97, 17);
            this.chkClientWarning.TabIndex = 4;
            this.chkClientWarning.Text = "Waarschuwing";
            this.chkClientWarning.UseVisualStyleBackColor = true;
            this.chkClientWarning.CheckedChanged += new System.EventHandler(this.chkClientWarning_CheckedChanged);
            // 
            // chkClientSuccess
            // 
            this.chkClientSuccess.AutoSize = true;
            this.chkClientSuccess.Location = new System.Drawing.Point(349, 76);
            this.chkClientSuccess.Name = "chkClientSuccess";
            this.chkClientSuccess.Size = new System.Drawing.Size(52, 17);
            this.chkClientSuccess.TabIndex = 5;
            this.chkClientSuccess.Text = "Goed";
            this.chkClientSuccess.UseVisualStyleBackColor = true;
            this.chkClientSuccess.CheckedChanged += new System.EventHandler(this.chkClientSuccess_CheckedChanged);
            // 
            // butClientPictFolder
            // 
            this.butClientPictFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butClientPictFolder.Location = new System.Drawing.Point(585, 121);
            this.butClientPictFolder.Name = "butClientPictFolder";
            this.butClientPictFolder.Size = new System.Drawing.Size(33, 23);
            this.butClientPictFolder.TabIndex = 10;
            this.butClientPictFolder.Text = "...";
            this.butClientPictFolder.UseVisualStyleBackColor = true;
            this.butClientPictFolder.Click += new System.EventHandler(this.butClientPictFolder_Click);
            // 
            // chkClientLogAppend
            // 
            this.chkClientLogAppend.AutoSize = true;
            this.chkClientLogAppend.Location = new System.Drawing.Point(129, 100);
            this.chkClientLogAppend.Name = "chkClientLogAppend";
            this.chkClientLogAppend.Size = new System.Drawing.Size(157, 17);
            this.chkClientLogAppend.TabIndex = 8;
            this.chkClientLogAppend.Text = "Toevoegen aan logbestand";
            this.chkClientLogAppend.UseVisualStyleBackColor = true;
            this.chkClientLogAppend.CheckedChanged += new System.EventHandler(this.chkClientLogAppend_CheckedChanged);
            // 
            // txtClientPictureFolder
            // 
            this.txtClientPictureFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientPictureFolder.Location = new System.Drawing.Point(129, 123);
            this.txtClientPictureFolder.Name = "txtClientPictureFolder";
            this.txtClientPictureFolder.Size = new System.Drawing.Size(453, 20);
            this.txtClientPictureFolder.TabIndex = 9;
            this.txtClientPictureFolder.TextChanged += new System.EventHandler(this.txtClientPictureFolder_TextChanged);
            this.txtClientPictureFolder.Enter += new System.EventHandler(this.txtClientPictureFolder_Enter);
            this.txtClientPictureFolder.Leave += new System.EventHandler(this.txtClientPictureFolder_Leave);
            // 
            // chkClientInfo
            // 
            this.chkClientInfo.AutoSize = true;
            this.chkClientInfo.Location = new System.Drawing.Point(408, 76);
            this.chkClientInfo.Name = "chkClientInfo";
            this.chkClientInfo.Size = new System.Drawing.Size(44, 17);
            this.chkClientInfo.TabIndex = 6;
            this.chkClientInfo.Text = "Info";
            this.chkClientInfo.UseVisualStyleBackColor = true;
            this.chkClientInfo.CheckedChanged += new System.EventHandler(this.chkClientInfo_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Map afbeeldingen";
            // 
            // chkClientDebug
            // 
            this.chkClientDebug.AutoSize = true;
            this.chkClientDebug.Location = new System.Drawing.Point(459, 76);
            this.chkClientDebug.Name = "chkClientDebug";
            this.chkClientDebug.Size = new System.Drawing.Size(58, 17);
            this.chkClientDebug.TabIndex = 7;
            this.chkClientDebug.Text = "Debug";
            this.chkClientDebug.UseVisualStyleBackColor = true;
            this.chkClientDebug.CheckedChanged += new System.EventHandler(this.chkClientDebug_CheckedChanged);
            // 
            // butRetrieveName
            // 
            this.butRetrieveName.Location = new System.Drawing.Point(362, 20);
            this.butRetrieveName.Name = "butRetrieveName";
            this.butRetrieveName.Size = new System.Drawing.Size(75, 23);
            this.butRetrieveName.TabIndex = 1;
            this.butRetrieveName.Text = "Ophalen";
            this.butRetrieveName.UseVisualStyleBackColor = true;
            this.butRetrieveName.Visible = false;
            this.butRetrieveName.Click += new System.EventHandler(this.butRetrieveName_Click);
            // 
            // txtUniqueComputerName
            // 
            this.txtUniqueComputerName.Location = new System.Drawing.Point(202, 20);
            this.txtUniqueComputerName.Name = "txtUniqueComputerName";
            this.txtUniqueComputerName.Size = new System.Drawing.Size(151, 20);
            this.txtUniqueComputerName.TabIndex = 0;
            this.txtUniqueComputerName.TextChanged += new System.EventHandler(this.txtUniqueComputerName_TextChanged);
            this.txtUniqueComputerName.Enter += new System.EventHandler(this.txtUniqueComputerName_Enter_1);
            this.txtUniqueComputerName.Leave += new System.EventHandler(this.txtUniqueComputerName_Leave_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Unieke naam van de client";
            // 
            // butIPOphalen
            // 
            this.butIPOphalen.Location = new System.Drawing.Point(362, 76);
            this.butIPOphalen.Name = "butIPOphalen";
            this.butIPOphalen.Size = new System.Drawing.Size(75, 23);
            this.butIPOphalen.TabIndex = 3;
            this.butIPOphalen.Text = "Ophalen";
            this.butIPOphalen.UseVisualStyleBackColor = true;
            this.butIPOphalen.Click += new System.EventHandler(this.butIPOphalen_Click);
            // 
            // txtIPThisComputer
            // 
            this.txtIPThisComputer.Location = new System.Drawing.Point(202, 76);
            this.txtIPThisComputer.Name = "txtIPThisComputer";
            this.txtIPThisComputer.Size = new System.Drawing.Size(151, 20);
            this.txtIPThisComputer.TabIndex = 2;
            this.txtIPThisComputer.TextChanged += new System.EventHandler(this.txtPortCentralServer_TextChanged);
            this.txtIPThisComputer.Enter += new System.EventHandler(this.txtIPThisComputer_Enter);
            this.txtIPThisComputer.Leave += new System.EventHandler(this.txtIPThisComputer_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "IP nummer van deze computer";
            // 
            // txtIpNumberServer
            // 
            this.txtIpNumberServer.Location = new System.Drawing.Point(202, 105);
            this.txtIpNumberServer.Name = "txtIpNumberServer";
            this.txtIpNumberServer.Size = new System.Drawing.Size(151, 20);
            this.txtIpNumberServer.TabIndex = 4;
            this.txtIpNumberServer.TextChanged += new System.EventHandler(this.txtIpNumberServer_TextChanged);
            this.txtIpNumberServer.Enter += new System.EventHandler(this.txtIpNumberServer_Enter);
            this.txtIpNumberServer.Leave += new System.EventHandler(this.txtIpNumberServer_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 108);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(166, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "IP nummer van de centrale server";
            // 
            // tabServer
            // 
            this.tabServer.Controls.Add(this.chkServerLogAppend);
            this.tabServer.Controls.Add(this.butServerPictureFolder);
            this.tabServer.Controls.Add(this.txtServerPictureFolder);
            this.tabServer.Controls.Add(this.label8);
            this.tabServer.Controls.Add(this.chkServerDebug);
            this.tabServer.Controls.Add(this.chkServerInfo);
            this.tabServer.Controls.Add(this.chkServerSuccess);
            this.tabServer.Controls.Add(this.chkServerWarning);
            this.tabServer.Controls.Add(this.chkServerError);
            this.tabServer.Controls.Add(this.chkServerFatal);
            this.tabServer.Controls.Add(this.label9);
            this.tabServer.Location = new System.Drawing.Point(4, 22);
            this.tabServer.Name = "tabServer";
            this.tabServer.Size = new System.Drawing.Size(645, 297);
            this.tabServer.TabIndex = 2;
            this.tabServer.Text = "Server";
            this.tabServer.UseVisualStyleBackColor = true;
            // 
            // chkServerLogAppend
            // 
            this.chkServerLogAppend.AutoSize = true;
            this.chkServerLogAppend.Location = new System.Drawing.Point(123, 37);
            this.chkServerLogAppend.Name = "chkServerLogAppend";
            this.chkServerLogAppend.Size = new System.Drawing.Size(157, 17);
            this.chkServerLogAppend.TabIndex = 6;
            this.chkServerLogAppend.Text = "Toevoegen aan logbestand";
            this.chkServerLogAppend.UseVisualStyleBackColor = true;
            this.chkServerLogAppend.CheckedChanged += new System.EventHandler(this.chkServerLogAppend_CheckedChanged);
            // 
            // butServerPictureFolder
            // 
            this.butServerPictureFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butServerPictureFolder.Location = new System.Drawing.Point(609, 55);
            this.butServerPictureFolder.Name = "butServerPictureFolder";
            this.butServerPictureFolder.Size = new System.Drawing.Size(33, 23);
            this.butServerPictureFolder.TabIndex = 8;
            this.butServerPictureFolder.Text = "...";
            this.butServerPictureFolder.UseVisualStyleBackColor = true;
            this.butServerPictureFolder.Click += new System.EventHandler(this.butServerPictureFolder_Click);
            // 
            // txtServerPictureFolder
            // 
            this.txtServerPictureFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerPictureFolder.Location = new System.Drawing.Point(123, 58);
            this.txtServerPictureFolder.Name = "txtServerPictureFolder";
            this.txtServerPictureFolder.Size = new System.Drawing.Size(484, 20);
            this.txtServerPictureFolder.TabIndex = 7;
            this.txtServerPictureFolder.TextChanged += new System.EventHandler(this.txtServerPictureFolder_TextChanged);
            this.txtServerPictureFolder.Enter += new System.EventHandler(this.txtServerPictureFolder_Enter);
            this.txtServerPictureFolder.Leave += new System.EventHandler(this.txtServerPictureFolder_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Map afbeeldingen";
            // 
            // chkServerDebug
            // 
            this.chkServerDebug.AutoSize = true;
            this.chkServerDebug.Location = new System.Drawing.Point(453, 13);
            this.chkServerDebug.Name = "chkServerDebug";
            this.chkServerDebug.Size = new System.Drawing.Size(58, 17);
            this.chkServerDebug.TabIndex = 5;
            this.chkServerDebug.Text = "Debug";
            this.chkServerDebug.UseVisualStyleBackColor = true;
            this.chkServerDebug.CheckedChanged += new System.EventHandler(this.chkServerDebug_CheckedChanged);
            // 
            // chkServerInfo
            // 
            this.chkServerInfo.AutoSize = true;
            this.chkServerInfo.Location = new System.Drawing.Point(402, 13);
            this.chkServerInfo.Name = "chkServerInfo";
            this.chkServerInfo.Size = new System.Drawing.Size(44, 17);
            this.chkServerInfo.TabIndex = 4;
            this.chkServerInfo.Text = "Info";
            this.chkServerInfo.UseVisualStyleBackColor = true;
            this.chkServerInfo.CheckedChanged += new System.EventHandler(this.chkServerInfo_CheckedChanged);
            // 
            // chkServerSuccess
            // 
            this.chkServerSuccess.AutoSize = true;
            this.chkServerSuccess.Location = new System.Drawing.Point(343, 13);
            this.chkServerSuccess.Name = "chkServerSuccess";
            this.chkServerSuccess.Size = new System.Drawing.Size(52, 17);
            this.chkServerSuccess.TabIndex = 3;
            this.chkServerSuccess.Text = "Goed";
            this.chkServerSuccess.UseVisualStyleBackColor = true;
            this.chkServerSuccess.CheckedChanged += new System.EventHandler(this.chkServerSuccess_CheckedChanged);
            // 
            // chkServerWarning
            // 
            this.chkServerWarning.AutoSize = true;
            this.chkServerWarning.Location = new System.Drawing.Point(239, 13);
            this.chkServerWarning.Name = "chkServerWarning";
            this.chkServerWarning.Size = new System.Drawing.Size(97, 17);
            this.chkServerWarning.TabIndex = 2;
            this.chkServerWarning.Text = "Waarschuwing";
            this.chkServerWarning.UseVisualStyleBackColor = true;
            this.chkServerWarning.CheckedChanged += new System.EventHandler(this.chkServerWarning_CheckedChanged);
            // 
            // chkServerError
            // 
            this.chkServerError.AutoSize = true;
            this.chkServerError.Location = new System.Drawing.Point(185, 13);
            this.chkServerError.Name = "chkServerError";
            this.chkServerError.Size = new System.Drawing.Size(47, 17);
            this.chkServerError.TabIndex = 1;
            this.chkServerError.Text = "Fout";
            this.chkServerError.UseVisualStyleBackColor = true;
            this.chkServerError.CheckedChanged += new System.EventHandler(this.chkServerError_CheckedChanged);
            // 
            // chkServerFatal
            // 
            this.chkServerFatal.AutoSize = true;
            this.chkServerFatal.Location = new System.Drawing.Point(123, 13);
            this.chkServerFatal.Name = "chkServerFatal";
            this.chkServerFatal.Size = new System.Drawing.Size(55, 17);
            this.chkServerFatal.TabIndex = 0;
            this.chkServerFatal.Text = "Fataal";
            this.chkServerFatal.UseVisualStyleBackColor = true;
            this.chkServerFatal.CheckedChanged += new System.EventHandler(this.chkServerFatal_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Niveau foutmeldingen";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBoxServer);
            this.tabPage1.Controls.Add(this.pictureBoxClient);
            this.tabPage1.Controls.Add(this.lbClientRunning);
            this.tabPage1.Controls.Add(this.lbServerRunning);
            this.tabPage1.Controls.Add(this.lblServerStatus);
            this.tabPage1.Controls.Add(this.lblClientStatus);
            this.tabPage1.Controls.Add(this.butRestartAll);
            this.tabPage1.Controls.Add(this.butRestartClient);
            this.tabPage1.Controls.Add(this.butStartStopServer);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.butRestartServer);
            this.tabPage1.Controls.Add(this.butStartStopClient);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(645, 297);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Services";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBoxServer
            // 
            this.pictureBoxServer.Location = new System.Drawing.Point(86, 23);
            this.pictureBoxServer.Name = "pictureBoxServer";
            this.pictureBoxServer.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxServer.TabIndex = 12;
            this.pictureBoxServer.TabStop = false;
            // 
            // pictureBoxClient
            // 
            this.pictureBoxClient.Location = new System.Drawing.Point(86, 58);
            this.pictureBoxClient.Name = "pictureBoxClient";
            this.pictureBoxClient.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxClient.TabIndex = 11;
            this.pictureBoxClient.TabStop = false;
            // 
            // lbClientRunning
            // 
            this.lbClientRunning.AutoSize = true;
            this.lbClientRunning.Location = new System.Drawing.Point(409, 103);
            this.lbClientRunning.Name = "lbClientRunning";
            this.lbClientRunning.Size = new System.Drawing.Size(81, 13);
            this.lbClientRunning.TabIndex = 10;
            this.lbClientRunning.Text = "lbClientRunning";
            // 
            // lbServerRunning
            // 
            this.lbServerRunning.AutoSize = true;
            this.lbServerRunning.Location = new System.Drawing.Point(409, 125);
            this.lbServerRunning.Name = "lbServerRunning";
            this.lbServerRunning.Size = new System.Drawing.Size(86, 13);
            this.lbServerRunning.TabIndex = 9;
            this.lbServerRunning.Text = "lbServerRunning";
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.AutoSize = true;
            this.lblServerStatus.Location = new System.Drawing.Point(409, 23);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(78, 13);
            this.lblServerStatus.TabIndex = 8;
            this.lblServerStatus.Text = "lblServerStatus";
            // 
            // lblClientStatus
            // 
            this.lblClientStatus.AutoSize = true;
            this.lblClientStatus.Location = new System.Drawing.Point(409, 56);
            this.lblClientStatus.Name = "lblClientStatus";
            this.lblClientStatus.Size = new System.Drawing.Size(73, 13);
            this.lblClientStatus.TabIndex = 7;
            this.lblClientStatus.Text = "lblClientStatus";
            // 
            // butRestartAll
            // 
            this.butRestartAll.Location = new System.Drawing.Point(288, 34);
            this.butRestartAll.Name = "butRestartAll";
            this.butRestartAll.Size = new System.Drawing.Size(75, 23);
            this.butRestartAll.TabIndex = 2;
            this.butRestartAll.Text = "Herstart Alle";
            this.butRestartAll.UseVisualStyleBackColor = true;
            this.butRestartAll.Visible = false;
            this.butRestartAll.Click += new System.EventHandler(this.butRestartAll_Click);
            // 
            // butRestartClient
            // 
            this.butRestartClient.Location = new System.Drawing.Point(189, 51);
            this.butRestartClient.Name = "butRestartClient";
            this.butRestartClient.Size = new System.Drawing.Size(75, 23);
            this.butRestartClient.TabIndex = 4;
            this.butRestartClient.Text = "Herstart";
            this.butRestartClient.UseVisualStyleBackColor = true;
            this.butRestartClient.Visible = false;
            this.butRestartClient.Click += new System.EventHandler(this.butRestartClient_Click);
            // 
            // butStartStopServer
            // 
            this.butStartStopServer.Location = new System.Drawing.Point(108, 18);
            this.butStartStopServer.Name = "butStartStopServer";
            this.butStartStopServer.Size = new System.Drawing.Size(75, 23);
            this.butStartStopServer.TabIndex = 0;
            this.butStartStopServer.Text = "Start";
            this.butStartStopServer.UseVisualStyleBackColor = true;
            this.butStartStopServer.Click += new System.EventHandler(this.butStartStopService_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Print Service";
            // 
            // butRestartServer
            // 
            this.butRestartServer.Location = new System.Drawing.Point(189, 18);
            this.butRestartServer.Name = "butRestartServer";
            this.butRestartServer.Size = new System.Drawing.Size(75, 23);
            this.butRestartServer.TabIndex = 1;
            this.butRestartServer.Text = "Herstart";
            this.butRestartServer.UseVisualStyleBackColor = true;
            this.butRestartServer.Visible = false;
            this.butRestartServer.Click += new System.EventHandler(this.butRestartServer_Click);
            // 
            // butStartStopClient
            // 
            this.butStartStopClient.Location = new System.Drawing.Point(108, 51);
            this.butStartStopClient.Name = "butStartStopClient";
            this.butStartStopClient.Size = new System.Drawing.Size(75, 23);
            this.butStartStopClient.TabIndex = 3;
            this.butStartStopClient.Text = "Start";
            this.butStartStopClient.UseVisualStyleBackColor = true;
            this.butStartStopClient.Click += new System.EventHandler(this.butStartStopClient_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Server Service";
            // 
            // butApply
            // 
            this.butApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butApply.Enabled = false;
            this.butApply.Location = new System.Drawing.Point(591, 338);
            this.butApply.Name = "butApply";
            this.butApply.Size = new System.Drawing.Size(75, 23);
            this.butApply.TabIndex = 1;
            this.butApply.Text = "&Toepassen";
            this.butApply.UseVisualStyleBackColor = true;
            this.butApply.Click += new System.EventHandler(this.butApply_Click);
            // 
            // butOK
            // 
            this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butOK.Location = new System.Drawing.Point(429, 338);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 2;
            this.butOK.Text = "&OK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Enabled = false;
            this.butCancel.Location = new System.Drawing.Point(510, 338);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 3;
            this.butCancel.Text = "&Annuleren";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butResetToInitialValues
            // 
            this.butResetToInitialValues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butResetToInitialValues.Location = new System.Drawing.Point(13, 338);
            this.butResetToInitialValues.Name = "butResetToInitialValues";
            this.butResetToInitialValues.Size = new System.Drawing.Size(137, 23);
            this.butResetToInitialValues.TabIndex = 4;
            this.butResetToInitialValues.Text = "Herstel orgineel";
            this.toolTip1.SetToolTip(this.butResetToInitialValues, "Herstel initiele waarden");
            this.butResetToInitialValues.UseVisualStyleBackColor = true;
            this.butResetToInitialValues.Click += new System.EventHandler(this.butResetToInitialValues_Click);
            // 
            // timerServiceStatus
            // 
            this.timerServiceStatus.Interval = 1000;
            this.timerServiceStatus.Tick += new System.EventHandler(this.timerServiceStatus_Tick);
            // 
            // imageListImages
            // 
            this.imageListImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListImages.ImageStream")));
            this.imageListImages.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListImages.Images.SetKeyName(0, "ProcessRunning.png");
            this.imageListImages.Images.SetKeyName(1, "button_cancel.png");
            this.imageListImages.Images.SetKeyName(2, "button_ok.png");
            this.imageListImages.Images.SetKeyName(3, "KillProcess");
            // 
            // frmMain
            // 
            this.AcceptButton = this.butOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 373);
            this.Controls.Add(this.butResetToInitialValues);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.butApply);
            this.Controls.Add(this.tabInstellingen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "ACA Labelprint Configuratie Programma";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabInstellingen.ResumeLayout(false);
            this.tabStats.ResumeLayout(false);
            this.tabStats.PerformLayout();
            this.tabAlgemeen.ResumeLayout(false);
            this.tabAlgemeen.PerformLayout();
            this.tabClient.ResumeLayout(false);
            this.tabClient.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabServer.ResumeLayout(false);
            this.tabServer.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabInstellingen;
        private System.Windows.Forms.TabPage tabAlgemeen;
        private System.Windows.Forms.TabPage tabClient;
        private System.Windows.Forms.TabPage tabServer;
        private System.Windows.Forms.Button butApply;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butSelectFolder;
        private System.Windows.Forms.TextBox txtDataFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkClientDebug;
        private System.Windows.Forms.CheckBox chkClientInfo;
        private System.Windows.Forms.CheckBox chkClientSuccess;
        private System.Windows.Forms.CheckBox chkClientWarning;
        private System.Windows.Forms.CheckBox chkClientError;
        private System.Windows.Forms.CheckBox chkClientFatal;
        private System.Windows.Forms.CheckBox chkClientLogAppend;
        private System.Windows.Forms.Button butClientPictFolder;
        private System.Windows.Forms.TextBox txtClientPictureFolder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIpNumberServer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkServerLogAppend;
        private System.Windows.Forms.Button butServerPictureFolder;
        private System.Windows.Forms.TextBox txtServerPictureFolder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkServerDebug;
        private System.Windows.Forms.CheckBox chkServerInfo;
        private System.Windows.Forms.CheckBox chkServerSuccess;
        private System.Windows.Forms.CheckBox chkServerWarning;
        private System.Windows.Forms.CheckBox chkServerError;
        private System.Windows.Forms.CheckBox chkServerFatal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button butRestartAll;
        private System.Windows.Forms.Button butRestartClient;
        private System.Windows.Forms.Button butStartStopServer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button butRestartServer;
        private System.Windows.Forms.Button butStartStopClient;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button butResetToInitialValues;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timerServiceStatus;
        private System.Windows.Forms.Label lblServerStatus;
        private System.Windows.Forms.Label lblClientStatus;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabPage tabStats;
        private System.Windows.Forms.CheckBox chkController;
        private System.Windows.Forms.CheckBox chkDesigner;
        private System.Windows.Forms.CheckBox chkClient;
        private System.Windows.Forms.CheckBox chkServer;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtLogFolder;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPortCentralServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPortPrintClient;
        private System.Windows.Forms.Button butRetrieveName;
        private System.Windows.Forms.TextBox txtUniqueComputerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button butIPOphalen;
        private System.Windows.Forms.TextBox txtIPThisComputer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIPServer;
        private System.Windows.Forms.Label lblIPNumberThis;
        private System.Windows.Forms.Label lbClientRunning;
        private System.Windows.Forms.Label lbServerRunning;
        private System.Windows.Forms.PictureBox pictureBoxServer;
        private System.Windows.Forms.PictureBox pictureBoxClient;
        private System.Windows.Forms.ImageList imageListImages;
        private System.Windows.Forms.CheckBox standaloneclientcheck;
    }
}

