namespace youtube_dl
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.UrlBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.DownloadStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.downloadSpeedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FiletypeBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.DownloadGrid = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VideoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.destinationBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.filenameBox = new System.Windows.Forms.TextBox();
            this.useTitleCheckbox = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.queueButton = new System.Windows.Forms.Button();
            this.downloadVideoWorker = new System.ComponentModel.BackgroundWorker();
            this.deleteButton = new System.Windows.Forms.Button();
            this.ThumbnailBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TitleCard = new System.Windows.Forms.Label();
            this.IDCard = new System.Windows.Forms.Label();
            this.FiletypeCard = new System.Windows.Forms.Label();
            this.PathCard = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portuguêsBrasileiroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayDownloadStatusTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddFromTextButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThumbnailBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UrlBox
            // 
            resources.ApplyResources(this.UrlBox, "UrlBox");
            this.UrlBox.Name = "UrlBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // DownloadButton
            // 
            resources.ApplyResources(this.DownloadButton, "DownloadButton");
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // StatusStrip
            // 
            resources.ApplyResources(this.StatusStrip, "StatusStrip");
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DownloadStatus,
            this.downloadSpeedLabel,
            this.toolStripStatusLabel1,
            this.statusLabel});
            this.StatusStrip.Name = "StatusStrip";
            // 
            // DownloadStatus
            // 
            resources.ApplyResources(this.DownloadStatus, "DownloadStatus");
            this.DownloadStatus.Name = "DownloadStatus";
            // 
            // downloadSpeedLabel
            // 
            resources.ApplyResources(this.downloadSpeedLabel, "downloadSpeedLabel");
            this.downloadSpeedLabel.Name = "downloadSpeedLabel";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // statusLabel
            // 
            resources.ApplyResources(this.statusLabel, "statusLabel");
            this.statusLabel.Name = "statusLabel";
            // 
            // FiletypeBox
            // 
            resources.ApplyResources(this.FiletypeBox, "FiletypeBox");
            this.FiletypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FiletypeBox.FormattingEnabled = true;
            this.FiletypeBox.Items.AddRange(new object[] {
            resources.GetString("FiletypeBox.Items"),
            resources.GetString("FiletypeBox.Items1"),
            resources.GetString("FiletypeBox.Items2"),
            resources.GetString("FiletypeBox.Items3"),
            resources.GetString("FiletypeBox.Items4"),
            resources.GetString("FiletypeBox.Items5"),
            resources.GetString("FiletypeBox.Items6"),
            resources.GetString("FiletypeBox.Items7"),
            resources.GetString("FiletypeBox.Items8"),
            resources.GetString("FiletypeBox.Items9"),
            resources.GetString("FiletypeBox.Items10"),
            resources.GetString("FiletypeBox.Items11"),
            resources.GetString("FiletypeBox.Items12"),
            resources.GetString("FiletypeBox.Items13"),
            resources.GetString("FiletypeBox.Items14"),
            resources.GetString("FiletypeBox.Items15"),
            resources.GetString("FiletypeBox.Items16"),
            resources.GetString("FiletypeBox.Items17")});
            this.FiletypeBox.Name = "FiletypeBox";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // DownloadGrid
            // 
            resources.ApplyResources(this.DownloadGrid, "DownloadGrid");
            this.DownloadGrid.AllowUserToAddRows = false;
            this.DownloadGrid.AllowUserToDeleteRows = false;
            this.DownloadGrid.AllowUserToOrderColumns = true;
            this.DownloadGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DownloadGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.Title,
            this.VideoID});
            this.DownloadGrid.MultiSelect = false;
            this.DownloadGrid.Name = "DownloadGrid";
            this.DownloadGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DownloadGrid.SelectionChanged += new System.EventHandler(this.DownloadGrid_SelectionChanged);
            // 
            // Number
            // 
            resources.ApplyResources(this.Number, "Number");
            this.Number.Name = "Number";
            // 
            // Title
            // 
            resources.ApplyResources(this.Title, "Title");
            this.Title.Name = "Title";
            // 
            // VideoID
            // 
            resources.ApplyResources(this.VideoID, "VideoID");
            this.VideoID.Name = "VideoID";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // destinationBox
            // 
            resources.ApplyResources(this.destinationBox, "destinationBox");
            this.destinationBox.Name = "destinationBox";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // filenameBox
            // 
            resources.ApplyResources(this.filenameBox, "filenameBox");
            this.filenameBox.Name = "filenameBox";
            // 
            // useTitleCheckbox
            // 
            resources.ApplyResources(this.useTitleCheckbox, "useTitleCheckbox");
            this.useTitleCheckbox.Checked = true;
            this.useTitleCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useTitleCheckbox.Name = "useTitleCheckbox";
            this.useTitleCheckbox.UseVisualStyleBackColor = true;
            this.useTitleCheckbox.CheckedChanged += new System.EventHandler(this.UseTitleCheckbox_CheckedChanged);
            // 
            // folderBrowserDialog
            // 
            resources.ApplyResources(this.folderBrowserDialog, "folderBrowserDialog");
            // 
            // queueButton
            // 
            resources.ApplyResources(this.queueButton, "queueButton");
            this.queueButton.Name = "queueButton";
            this.queueButton.UseVisualStyleBackColor = true;
            this.queueButton.Click += new System.EventHandler(this.QueueButton_Click);
            // 
            // downloadVideoWorker
            // 
            this.downloadVideoWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DownloadVideoWorker_DoWork);
            // 
            // deleteButton
            // 
            resources.ApplyResources(this.deleteButton, "deleteButton");
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ThumbnailBox
            // 
            resources.ApplyResources(this.ThumbnailBox, "ThumbnailBox");
            this.ThumbnailBox.Name = "ThumbnailBox";
            this.ThumbnailBox.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // TitleCard
            // 
            resources.ApplyResources(this.TitleCard, "TitleCard");
            this.TitleCard.Name = "TitleCard";
            // 
            // IDCard
            // 
            resources.ApplyResources(this.IDCard, "IDCard");
            this.IDCard.Name = "IDCard";
            // 
            // FiletypeCard
            // 
            resources.ApplyResources(this.FiletypeCard, "FiletypeCard");
            this.FiletypeCard.Name = "FiletypeCard";
            // 
            // PathCard
            // 
            resources.ApplyResources(this.PathCard, "PathCard");
            this.PathCard.Name = "PathCard";
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem,
            this.displayDownloadStatusTextToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            // 
            // languageToolStripMenuItem
            // 
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.portuguêsBrasileiroToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            // 
            // portuguêsBrasileiroToolStripMenuItem
            // 
            resources.ApplyResources(this.portuguêsBrasileiroToolStripMenuItem, "portuguêsBrasileiroToolStripMenuItem");
            this.portuguêsBrasileiroToolStripMenuItem.Name = "portuguêsBrasileiroToolStripMenuItem";
            this.portuguêsBrasileiroToolStripMenuItem.Click += new System.EventHandler(this.PortuguêsBrasileiroToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.EnglishToolStripMenuItem_Click);
            // 
            // displayDownloadStatusTextToolStripMenuItem
            // 
            resources.ApplyResources(this.displayDownloadStatusTextToolStripMenuItem, "displayDownloadStatusTextToolStripMenuItem");
            this.displayDownloadStatusTextToolStripMenuItem.Name = "displayDownloadStatusTextToolStripMenuItem";
            this.displayDownloadStatusTextToolStripMenuItem.Click += new System.EventHandler(this.DisplayDownloadStatusTextToolStripMenuItem_Click);
            // 
            // AddFromTextButton
            // 
            resources.ApplyResources(this.AddFromTextButton, "AddFromTextButton");
            this.AddFromTextButton.Name = "AddFromTextButton";
            this.AddFromTextButton.UseVisualStyleBackColor = true;
            this.AddFromTextButton.Click += new System.EventHandler(this.AddFromTextButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AddFromTextButton);
            this.Controls.Add(this.PathCard);
            this.Controls.Add(this.FiletypeCard);
            this.Controls.Add(this.IDCard);
            this.Controls.Add(this.TitleCard);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ThumbnailBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.queueButton);
            this.Controls.Add(this.useTitleCheckbox);
            this.Controls.Add(this.filenameBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.destinationBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DownloadGrid);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FiletypeBox);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UrlBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThumbnailBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UrlBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ComboBox FiletypeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView DownloadGrid;
        private System.Windows.Forms.ToolStripStatusLabel DownloadStatus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox destinationBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox filenameBox;
        private System.Windows.Forms.CheckBox useTitleCheckbox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripStatusLabel downloadSpeedLabel;
        private System.Windows.Forms.Button queueButton;
        private System.ComponentModel.BackgroundWorker downloadVideoWorker;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.PictureBox ThumbnailBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label TitleCard;
        private System.Windows.Forms.Label IDCard;
        private System.Windows.Forms.Label FiletypeCard;
        private System.Windows.Forms.Label PathCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn VideoID;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portuguêsBrasileiroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayDownloadStatusTextToolStripMenuItem;
        private System.Windows.Forms.Button AddFromTextButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

