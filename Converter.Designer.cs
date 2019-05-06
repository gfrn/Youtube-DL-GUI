namespace youtube_dl
{
    partial class Converter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Converter));
            this.label1 = new System.Windows.Forms.Label();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ConvertFromToLabel = new System.Windows.Forms.Label();
            this.OutputFileLabel = new System.Windows.Forms.Label();
            this.OriginalFileLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.SaveFileButton = new System.Windows.Forms.Button();
            this.CutStartTextbox = new System.Windows.Forms.MaskedTextBox();
            this.CutEndTextbox = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.EndOfVideoCheckbox = new System.Windows.Forms.CheckBox();
            this.JoinVideosButton = new System.Windows.Forms.Button();
            this.AddSubsButton = new System.Windows.Forms.Button();
            this.openSubtitlesDialog = new System.Windows.Forms.OpenFileDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.CancelJoinButton = new System.Windows.Forms.Button();
            this.CancelSubtitlesButton = new System.Windows.Forms.Button();
            this.CancelMergeButton = new System.Windows.Forms.Button();
            this.MergeButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.openVideoListDialog = new System.Windows.Forms.OpenFileDialog();
            this.CancelImportButton = new System.Windows.Forms.Button();
            this.openMergeDialog = new System.Windows.Forms.OpenFileDialog();
            this.IntervalSnagBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // OpenFileButton
            // 
            resources.ApplyResources(this.OpenFileButton, "OpenFileButton");
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // statusLabel
            // 
            resources.ApplyResources(this.statusLabel, "statusLabel");
            this.statusLabel.Name = "statusLabel";
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // ConvertButton
            // 
            resources.ApplyResources(this.ConvertButton, "ConvertButton");
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.ConvertFromToLabel);
            this.groupBox1.Controls.Add(this.OutputFileLabel);
            this.groupBox1.Controls.Add(this.OriginalFileLabel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ConvertFromToLabel
            // 
            resources.ApplyResources(this.ConvertFromToLabel, "ConvertFromToLabel");
            this.ConvertFromToLabel.Name = "ConvertFromToLabel";
            // 
            // OutputFileLabel
            // 
            resources.ApplyResources(this.OutputFileLabel, "OutputFileLabel");
            this.OutputFileLabel.Name = "OutputFileLabel";
            // 
            // OriginalFileLabel
            // 
            resources.ApplyResources(this.OriginalFileLabel, "OriginalFileLabel");
            this.OriginalFileLabel.Name = "OriginalFileLabel";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // saveFileDialog
            // 
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            this.saveFileDialog.OverwritePrompt = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // SaveFileButton
            // 
            resources.ApplyResources(this.SaveFileButton, "SaveFileButton");
            this.SaveFileButton.Name = "SaveFileButton";
            this.SaveFileButton.UseVisualStyleBackColor = true;
            this.SaveFileButton.Click += new System.EventHandler(this.SaveFileButton_Click);
            // 
            // CutStartTextbox
            // 
            resources.ApplyResources(this.CutStartTextbox, "CutStartTextbox");
            this.CutStartTextbox.BeepOnError = true;
            this.CutStartTextbox.Name = "CutStartTextbox";
            // 
            // CutEndTextbox
            // 
            resources.ApplyResources(this.CutEndTextbox, "CutEndTextbox");
            this.CutEndTextbox.Name = "CutEndTextbox";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // EndOfVideoCheckbox
            // 
            resources.ApplyResources(this.EndOfVideoCheckbox, "EndOfVideoCheckbox");
            this.EndOfVideoCheckbox.Checked = true;
            this.EndOfVideoCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EndOfVideoCheckbox.Name = "EndOfVideoCheckbox";
            this.EndOfVideoCheckbox.UseVisualStyleBackColor = true;
            this.EndOfVideoCheckbox.CheckedChanged += new System.EventHandler(this.EndOfVideoCheckbox_CheckedChanged);
            // 
            // JoinVideosButton
            // 
            resources.ApplyResources(this.JoinVideosButton, "JoinVideosButton");
            this.JoinVideosButton.Name = "JoinVideosButton";
            this.JoinVideosButton.UseVisualStyleBackColor = true;
            this.JoinVideosButton.Click += new System.EventHandler(this.JoinVideosButton_Click);
            // 
            // AddSubsButton
            // 
            resources.ApplyResources(this.AddSubsButton, "AddSubsButton");
            this.AddSubsButton.Name = "AddSubsButton";
            this.AddSubsButton.UseVisualStyleBackColor = true;
            this.AddSubsButton.Click += new System.EventHandler(this.AddSubsButton_Click);
            // 
            // openSubtitlesDialog
            // 
            resources.ApplyResources(this.openSubtitlesDialog, "openSubtitlesDialog");
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // CancelJoinButton
            // 
            resources.ApplyResources(this.CancelJoinButton, "CancelJoinButton");
            this.CancelJoinButton.Name = "CancelJoinButton";
            this.CancelJoinButton.UseVisualStyleBackColor = true;
            this.CancelJoinButton.Click += new System.EventHandler(this.CancelJoinButton_Click);
            // 
            // CancelSubtitlesButton
            // 
            resources.ApplyResources(this.CancelSubtitlesButton, "CancelSubtitlesButton");
            this.CancelSubtitlesButton.Name = "CancelSubtitlesButton";
            this.CancelSubtitlesButton.UseVisualStyleBackColor = true;
            // 
            // CancelMergeButton
            // 
            resources.ApplyResources(this.CancelMergeButton, "CancelMergeButton");
            this.CancelMergeButton.Name = "CancelMergeButton";
            this.CancelMergeButton.UseVisualStyleBackColor = true;
            this.CancelMergeButton.Click += new System.EventHandler(this.CancelMergeButton_Click);
            // 
            // MergeButton
            // 
            resources.ApplyResources(this.MergeButton, "MergeButton");
            this.MergeButton.Name = "MergeButton";
            this.MergeButton.UseVisualStyleBackColor = true;
            this.MergeButton.Click += new System.EventHandler(this.MergeButton_Click);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // openVideoListDialog
            // 
            resources.ApplyResources(this.openVideoListDialog, "openVideoListDialog");
            // 
            // CancelImportButton
            // 
            resources.ApplyResources(this.CancelImportButton, "CancelImportButton");
            this.CancelImportButton.Name = "CancelImportButton";
            this.CancelImportButton.UseVisualStyleBackColor = true;
            this.CancelImportButton.Click += new System.EventHandler(this.CancelImportButton_Click);
            // 
            // openMergeDialog
            // 
            resources.ApplyResources(this.openMergeDialog, "openMergeDialog");
            // 
            // IntervalSnagBox
            // 
            resources.ApplyResources(this.IntervalSnagBox, "IntervalSnagBox");
            this.IntervalSnagBox.Name = "IntervalSnagBox";
            this.IntervalSnagBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IntervalSnagBox_KeyPress);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // Converter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.IntervalSnagBox);
            this.Controls.Add(this.CancelImportButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.CancelMergeButton);
            this.Controls.Add(this.CancelSubtitlesButton);
            this.Controls.Add(this.CancelJoinButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.MergeButton);
            this.Controls.Add(this.AddSubsButton);
            this.Controls.Add(this.JoinVideosButton);
            this.Controls.Add(this.EndOfVideoCheckbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CutEndTextbox);
            this.Controls.Add(this.CutStartTextbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ConvertButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.SaveFileButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OpenFileButton);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Converter";
            this.ShowIcon = false;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label ConvertFromToLabel;
        private System.Windows.Forms.Label OutputFileLabel;
        private System.Windows.Forms.Label OriginalFileLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button SaveFileButton;
        private System.Windows.Forms.MaskedTextBox CutStartTextbox;
        private System.Windows.Forms.MaskedTextBox CutEndTextbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox EndOfVideoCheckbox;
        private System.Windows.Forms.Button JoinVideosButton;
        private System.Windows.Forms.Button AddSubsButton;
        private System.Windows.Forms.OpenFileDialog openSubtitlesDialog;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button CancelJoinButton;
        private System.Windows.Forms.Button CancelSubtitlesButton;
        private System.Windows.Forms.Button CancelMergeButton;
        private System.Windows.Forms.Button MergeButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.OpenFileDialog openVideoListDialog;
        private System.Windows.Forms.Button CancelImportButton;
        private System.Windows.Forms.OpenFileDialog openMergeDialog;
        private System.Windows.Forms.TextBox IntervalSnagBox;
        private System.Windows.Forms.Label label9;
    }
}