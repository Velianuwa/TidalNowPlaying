namespace TidalNowPlaying
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button toggleButton;
        private Label statusLabel;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.CheckBox minimizeToTrayCheckbox;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.toggleButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.minimizeToTrayCheckbox = new System.Windows.Forms.CheckBox();
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip();
            this.trayIcon = new System.Windows.Forms.NotifyIcon();

            this.SuspendLayout();

            // toggleButton
            this.toggleButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toggleButton.Location = new System.Drawing.Point(40, 30);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(180, 40);
            this.toggleButton.TabIndex = 0;
            this.toggleButton.Text = "Enable";
            this.toggleButton.UseVisualStyleBackColor = true;
            this.toggleButton.Click += new System.EventHandler(this.toggleButton_Click);

            // statusLabel
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.statusLabel.Location = new System.Drawing.Point(40, 85);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(114, 19);
            this.statusLabel.Text = "Status: Disabled";

            // minimizeToTrayCheckbox
            this.minimizeToTrayCheckbox.AutoSize = true;
            this.minimizeToTrayCheckbox.Location = new System.Drawing.Point(40, 110);
            this.minimizeToTrayCheckbox.Name = "minimizeToTrayCheckbox";
            this.minimizeToTrayCheckbox.Size = new System.Drawing.Size(113, 19);
            this.minimizeToTrayCheckbox.Text = "Minimize to tray";
            this.minimizeToTrayCheckbox.Checked = false;

            // trayIcon
            this.trayIcon.Text = "Tidal Now Playing";
            this.trayIcon.ContextMenuStrip = this.trayMenu;
            this.trayIcon.Visible = true;

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(264, 150);
            this.Controls.Add(this.toggleButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.minimizeToTrayCheckbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Tidal Now Playing";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

    }
}
