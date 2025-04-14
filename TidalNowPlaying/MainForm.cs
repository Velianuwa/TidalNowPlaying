using System.Diagnostics;
using System.Reflection;

namespace TidalNowPlaying
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer songTimer = new System.Windows.Forms.Timer();
        private bool isEnabled = false;
        private string _currentSongInfo = string.Empty;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string resourceName = "TidalNowPlaying.Assets.Tray.ico";

                using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        Icon embeddedIcon = new Icon(stream);
                        this.Icon = embeddedIcon;
                        this.trayIcon.Icon = embeddedIcon;
                    }
                    else
                    {
                        MessageBox.Show($"It was not possible to find the embedded resource: {resourceName}", "Error finding resource", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Icon = SystemIcons.Application;
                        this.trayIcon.Icon = SystemIcons.Application;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading the embedded icon: {ex.Message}", "Icon error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Icon = SystemIcons.Application;
                this.trayIcon.Icon = SystemIcons.Application;
            }

            songTimer.Interval = 10000;
            songTimer.Tick += SongTimer_Tick!;

            trayMenu.Items.Add("Exit", null, OnExit!);

            trayIcon.DoubleClick += (s, e) => {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            };

            this.Resize += (s, e) =>
            {
                if (this.WindowState == FormWindowState.Minimized && minimizeToTrayCheckbox.Checked)
                {
                    this.Hide();
                }
            };

            UpdateTrayToolTip();
        }

        private void toggleButton_Click(object sender, EventArgs e)
        {
            isEnabled = !isEnabled;
            toggleButton.Text = isEnabled ? "Disable" : "Enable";
            statusLabel.Text = isEnabled ? "Status: Enabled" : "Status: Disabled";

            if (isEnabled)
            {
                songTimer.Start();
                SongTimer_Tick(null, EventArgs.Empty);
            }
            else
            {
                songTimer.Stop();
                _currentSongInfo = string.Empty;
            }

            UpdateTrayToolTip();
        }

        private void UpdateTrayToolTip()
        {
            string status = isEnabled ? "Enabled" : "Disabled";
            string song = string.IsNullOrEmpty(_currentSongInfo) ? "No song playing" : _currentSongInfo;

            string toolTipText = $"Tidal Now Playing - Status: {status}";
            if (isEnabled && !string.IsNullOrEmpty(song))
            {
                toolTipText += $" - Playing: {song}";
            }

            if (toolTipText.Length >= 128)
            {
                toolTipText = toolTipText.Substring(0, 124) + "...";
            }

            if (trayIcon != null)
            {
                trayIcon.Text = toolTipText;
            }
        }

        private void SongTimer_Tick(object? sender, EventArgs e)
        {
            string songInfo = GetTidalWindowTitle();

            if (_currentSongInfo != songInfo)
            {
                _currentSongInfo = songInfo;
                UpdateTrayToolTip();
            }


            if (!string.IsNullOrEmpty(songInfo))
            {
                string formatted = $"Current Song: {songInfo}";
                try
                {
                    string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TidalNowPlaying");
                    Directory.CreateDirectory(appDataFolder);
                    string filePath = Path.Combine(appDataFolder, "CurrentSongDetails.txt");
                    File.WriteAllText(filePath, formatted);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing file: {ex.Message}");
                }
            }
        }

        private string GetTidalWindowTitle()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (!string.IsNullOrEmpty(p.MainWindowTitle) && p.ProcessName.ToLower().Contains("tidal"))
                {
                    if (p.MainWindowTitle.Contains(" - "))
                    {
                        return p.MainWindowTitle;
                    }
                }
            }
            return string.Empty;
        }

        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
