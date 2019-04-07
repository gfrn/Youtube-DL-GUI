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
            this.components = new System.ComponentModel.Container();
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
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.destinationBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.filenameBox = new System.Windows.Forms.TextBox();
            this.useTitleCheckbox = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.queueButton = new System.Windows.Forms.Button();
            this.downloadVideoWorker = new System.ComponentModel.BackgroundWorker();
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
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayVerboseStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alertOnFinishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.converterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddFromTextButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.EditIDBox = new System.Windows.Forms.TextBox();
            this.EditDestinationButton = new System.Windows.Forms.Button();
            this.EditFiletypeBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.EditFilenameBox = new System.Windows.Forms.TextBox();
            this.FilenameCard = new System.Windows.Forms.Label();
            this.UseTitleInEditCheckbox = new System.Windows.Forms.CheckBox();
            this.ExportButton = new System.Windows.Forms.Button();
            this.exportQueueDialog = new System.Windows.Forms.SaveFileDialog();
            this.StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadGrid)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
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
            resources.GetString("FiletypeBox.Items16")});
            this.FiletypeBox.Name = "FiletypeBox";
            this.FiletypeBox.SelectedIndexChanged += new System.EventHandler(this.FiletypeBox_SelectedIndexChanged);
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
            this.DownloadGrid.AllowUserToOrderColumns = true;
            this.DownloadGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.DownloadGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DownloadGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.Title,
            this.VideoID});
            this.DownloadGrid.ContextMenuStrip = this.contextMenuStrip;
            this.DownloadGrid.MultiSelect = false;
            this.DownloadGrid.Name = "DownloadGrid";
            this.DownloadGrid.ReadOnly = true;
            this.DownloadGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DownloadGrid.SelectionChanged += new System.EventHandler(this.DownloadGrid_SelectionChanged);
            this.DownloadGrid.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DownloadGrid_UserDeletedRow);
            this.DownloadGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DownloadGrid_MouseUp);
            // 
            // Number
            // 
            resources.ApplyResources(this.Number, "Number");
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            // 
            // Title
            // 
            resources.ApplyResources(this.Title, "Title");
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // VideoID
            // 
            resources.ApplyResources(this.VideoID, "VideoID");
            this.VideoID.Name = "VideoID";
            this.VideoID.ReadOnly = true;
            // 
            // contextMenuStrip
            // 
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteContextMenuItem,
            this.editStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            // 
            // deleteContextMenuItem
            // 
            resources.ApplyResources(this.deleteContextMenuItem, "deleteContextMenuItem");
            this.deleteContextMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteContextMenuItem.Name = "deleteContextMenuItem";
            this.deleteContextMenuItem.Click += new System.EventHandler(this.deleteContextMenuItem_Click);
            // 
            // editStripMenuItem
            // 
            resources.ApplyResources(this.editStripMenuItem, "editStripMenuItem");
            this.editStripMenuItem.Name = "editStripMenuItem";
            this.editStripMenuItem.Click += new System.EventHandler(this.editStripMenuItem_Click);
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
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem,
            this.displayToolStripMenuItem});
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
            // displayToolStripMenuItem
            // 
            resources.ApplyResources(this.displayToolStripMenuItem, "displayToolStripMenuItem");
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayVerboseStatusToolStripMenuItem,
            this.alertOnFinishToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            // 
            // displayVerboseStatusToolStripMenuItem
            // 
            resources.ApplyResources(this.displayVerboseStatusToolStripMenuItem, "displayVerboseStatusToolStripMenuItem");
            this.displayVerboseStatusToolStripMenuItem.CheckOnClick = true;
            this.displayVerboseStatusToolStripMenuItem.Name = "displayVerboseStatusToolStripMenuItem";
            // 
            // alertOnFinishToolStripMenuItem
            // 
            resources.ApplyResources(this.alertOnFinishToolStripMenuItem, "alertOnFinishToolStripMenuItem");
            this.alertOnFinishToolStripMenuItem.CheckOnClick = true;
            this.alertOnFinishToolStripMenuItem.Name = "alertOnFinishToolStripMenuItem";
            // 
            // aboutToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.converterToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            // 
            // converterToolStripMenuItem
            // 
            resources.ApplyResources(this.converterToolStripMenuItem, "converterToolStripMenuItem");
            this.converterToolStripMenuItem.Name = "converterToolStripMenuItem";
            this.converterToolStripMenuItem.Click += new System.EventHandler(this.converterToolStripMenuItem_Click);
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
            // EditIDBox
            // 
            resources.ApplyResources(this.EditIDBox, "EditIDBox");
            this.EditIDBox.Name = "EditIDBox";
            // 
            // EditDestinationButton
            // 
            resources.ApplyResources(this.EditDestinationButton, "EditDestinationButton");
            this.EditDestinationButton.Name = "EditDestinationButton";
            this.EditDestinationButton.UseVisualStyleBackColor = true;
            this.EditDestinationButton.Click += new System.EventHandler(this.EditDestinationButton_Click);
            // 
            // EditFiletypeBox
            // 
            resources.ApplyResources(this.EditFiletypeBox, "EditFiletypeBox");
            this.EditFiletypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EditFiletypeBox.FormattingEnabled = true;
            this.EditFiletypeBox.Items.AddRange(new object[] {
            resources.GetString("EditFiletypeBox.Items"),
            resources.GetString("EditFiletypeBox.Items1"),
            resources.GetString("EditFiletypeBox.Items2"),
            resources.GetString("EditFiletypeBox.Items3"),
            resources.GetString("EditFiletypeBox.Items4"),
            resources.GetString("EditFiletypeBox.Items5"),
            resources.GetString("EditFiletypeBox.Items6"),
            resources.GetString("EditFiletypeBox.Items7"),
            resources.GetString("EditFiletypeBox.Items8"),
            resources.GetString("EditFiletypeBox.Items9"),
            resources.GetString("EditFiletypeBox.Items10"),
            resources.GetString("EditFiletypeBox.Items11"),
            resources.GetString("EditFiletypeBox.Items12"),
            resources.GetString("EditFiletypeBox.Items13"),
            resources.GetString("EditFiletypeBox.Items14"),
            resources.GetString("EditFiletypeBox.Items15"),
            resources.GetString("EditFiletypeBox.Items16")});
            this.EditFiletypeBox.Name = "EditFiletypeBox";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // EditFilenameBox
            // 
            resources.ApplyResources(this.EditFilenameBox, "EditFilenameBox");
            this.EditFilenameBox.Name = "EditFilenameBox";
            // 
            // FilenameCard
            // 
            resources.ApplyResources(this.FilenameCard, "FilenameCard");
            this.FilenameCard.Name = "FilenameCard";
            // 
            // UseTitleInEditCheckbox
            // 
            resources.ApplyResources(this.UseTitleInEditCheckbox, "UseTitleInEditCheckbox");
            this.UseTitleInEditCheckbox.Name = "UseTitleInEditCheckbox";
            this.UseTitleInEditCheckbox.UseVisualStyleBackColor = true;
            this.UseTitleInEditCheckbox.CheckedChanged += new System.EventHandler(this.UseTitleInEditCheckbox_CheckedChanged);
            // 
            // ExportButton
            // 
            resources.ApplyResources(this.ExportButton, "ExportButton");
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // exportQueueDialog
            // 
            resources.ApplyResources(this.exportQueueDialog, "exportQueueDialog");
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.UseTitleInEditCheckbox);
            this.Controls.Add(this.FilenameCard);
            this.Controls.Add(this.EditFilenameBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.EditFiletypeBox);
            this.Controls.Add(this.EditDestinationButton);
            this.Controls.Add(this.EditIDBox);
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
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadGrid)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.Button AddFromTextButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayVerboseStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alertOnFinishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editStripMenuItem;
        private System.Windows.Forms.TextBox EditIDBox;
        private System.Windows.Forms.Button EditDestinationButton;
        private System.Windows.Forms.ComboBox EditFiletypeBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox EditFilenameBox;
        private System.Windows.Forms.Label FilenameCard;
        private System.Windows.Forms.CheckBox UseTitleInEditCheckbox;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem converterToolStripMenuItem;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.SaveFileDialog exportQueueDialog;
    }
}

