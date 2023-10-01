namespace SINCAdvancedModManager {
    partial class StartForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            LabelLoaderInstallationStatus = new Label();
            ButtonInstallLoader = new Button();
            statusStrip1 = new StatusStrip();
            ProgessBarState = new ToolStripProgressBar();
            LabelState = new ToolStripStatusLabel();
            ListBoxMods = new ListBox();
            LabelInstalled = new Label();
            LabelModTitle = new Label();
            ButtonRemove = new Button();
            ButtonInstallFromZip = new Button();
            OpenFileDialogZipFile = new OpenFileDialog();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // LabelLoaderInstallationStatus
            // 
            LabelLoaderInstallationStatus.AutoSize = true;
            LabelLoaderInstallationStatus.Location = new Point(13, 13);
            LabelLoaderInstallationStatus.Margin = new Padding(4, 0, 4, 0);
            LabelLoaderInstallationStatus.Name = "LabelLoaderInstallationStatus";
            LabelLoaderInstallationStatus.Size = new Size(145, 21);
            LabelLoaderInstallationStatus.TabIndex = 0;
            LabelLoaderInstallationStatus.Text = "Loader installed: no";
            // 
            // ButtonInstallLoader
            // 
            ButtonInstallLoader.Location = new Point(629, 13);
            ButtonInstallLoader.Name = "ButtonInstallLoader";
            ButtonInstallLoader.Size = new Size(162, 36);
            ButtonInstallLoader.TabIndex = 1;
            ButtonInstallLoader.Text = "Install Loader/Sync";
            ButtonInstallLoader.UseVisualStyleBackColor = true;
            ButtonInstallLoader.Click += ButtonInstallLoader_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { ProgessBarState, LabelState });
            statusStrip1.Location = new Point(0, 388);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(803, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // ProgessBarState
            // 
            ProgessBarState.Name = "ProgessBarState";
            ProgessBarState.Size = new Size(250, 16);
            // 
            // LabelState
            // 
            LabelState.Margin = new Padding(10, 3, 0, 2);
            LabelState.Name = "LabelState";
            LabelState.Size = new Size(116, 17);
            LabelState.Text = "State: Doing nothing";
            // 
            // ListBoxMods
            // 
            ListBoxMods.FormattingEnabled = true;
            ListBoxMods.ItemHeight = 21;
            ListBoxMods.Location = new Point(12, 65);
            ListBoxMods.Name = "ListBoxMods";
            ListBoxMods.Size = new Size(305, 277);
            ListBoxMods.TabIndex = 3;
            // 
            // LabelInstalled
            // 
            LabelInstalled.AutoSize = true;
            LabelInstalled.Location = new Point(12, 41);
            LabelInstalled.Name = "LabelInstalled";
            LabelInstalled.Size = new Size(115, 21);
            LabelInstalled.TabIndex = 4;
            LabelInstalled.Text = "Isntalled mods:";
            // 
            // LabelModTitle
            // 
            LabelModTitle.AutoSize = true;
            LabelModTitle.Font = new Font("Segoe UI Variable Display", 16F, FontStyle.Regular, GraphicsUnit.Point);
            LabelModTitle.Location = new Point(323, 65);
            LabelModTitle.Name = "LabelModTitle";
            LabelModTitle.Size = new Size(460, 30);
            LabelModTitle.TabIndex = 5;
            LabelModTitle.Text = "Click an installed mod to see more information";
            // 
            // ButtonRemove
            // 
            ButtonRemove.Location = new Point(323, 345);
            ButtonRemove.Name = "ButtonRemove";
            ButtonRemove.Size = new Size(460, 40);
            ButtonRemove.TabIndex = 8;
            ButtonRemove.Text = "Remove";
            ButtonRemove.UseVisualStyleBackColor = true;
            ButtonRemove.Click += ButtonRemove_Click;
            // 
            // ButtonInstallFromZip
            // 
            ButtonInstallFromZip.Location = new Point(12, 345);
            ButtonInstallFromZip.Name = "ButtonInstallFromZip";
            ButtonInstallFromZip.Size = new Size(305, 40);
            ButtonInstallFromZip.TabIndex = 9;
            ButtonInstallFromZip.Text = "Install from zip file";
            ButtonInstallFromZip.UseVisualStyleBackColor = true;
            ButtonInstallFromZip.Click += ButtonInstallFromZip_Click;
            // 
            // OpenFileDialogZipFile
            // 
            OpenFileDialogZipFile.Filter = "ZIP-Files|*.zip";
            OpenFileDialogZipFile.FileOk += OpenFileDialogZipFile_FileOk;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(803, 410);
            Controls.Add(ButtonInstallFromZip);
            Controls.Add(ButtonRemove);
            Controls.Add(LabelModTitle);
            Controls.Add(LabelInstalled);
            Controls.Add(ListBoxMods);
            Controls.Add(statusStrip1);
            Controls.Add(ButtonInstallLoader);
            Controls.Add(LabelLoaderInstallationStatus);
            Font = new Font("Segoe UI Variable Display", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4);
            Name = "StartForm";
            Text = "MainForm";
            Load += StartForm_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LabelLoaderInstallationStatus;
        private Button ButtonInstallLoader;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar ProgessBarState;
        private ToolStripStatusLabel LabelState;
        private ListBox ListBoxMods;
        private Label LabelInstalled;
        private Label LabelModTitle;
        private Button ButtonRemove;
        private Button ButtonInstallFromZip;
        private OpenFileDialog OpenFileDialogZipFile;
    }
}