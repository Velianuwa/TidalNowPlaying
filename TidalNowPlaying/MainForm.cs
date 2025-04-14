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
                // Obter o assembly atual (seu .exe)
                var assembly = Assembly.GetExecutingAssembly();
                // Construir o nome completo do recurso (Namespace.Pasta.NomeDoArquivo)
                // Assumindo que o namespace padr�o � "TidalNowPlaying" e o �cone est� na pasta "Assets"
                string resourceName = "TidalNowPlaying.Assets.Tray.ico";

                // Tentar carregar o stream do recurso
                using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        // Criar o �cone a partir do stream
                        Icon embeddedIcon = new Icon(stream);
                        this.Icon = embeddedIcon;          // Define o �cone do formul�rio
                        this.trayIcon.Icon = embeddedIcon; // Define o �cone da bandeja
                    }
                    else
                    {
                        // O recurso n�o foi encontrado - talvez o nome esteja errado?
                        MessageBox.Show($"N�o foi poss�vel encontrar o recurso incorporado: {resourceName}", "Erro de Recurso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // Pode definir um �cone padr�o ou deixar sem, se preferir
                        // this.Icon = SystemIcons.Application;
                        // this.trayIcon.Icon = SystemIcons.Application;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar o �cone incorporado: {ex.Message}", "Erro de �cone", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Lidar com o erro, talvez definir um �cone padr�o
                // this.Icon = SystemIcons.Application;
                // this.trayIcon.Icon = SystemIcons.Application;
            }

            songTimer.Interval = 10000;
            songTimer.Tick += SongTimer_Tick!;

            // Setup tray menu
            trayMenu.Items.Add("Exit", null, OnExit!);

            // Tray icon double click restore
            trayIcon.DoubleClick += (s, e) => {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            };

            // Minimize to tray behavior
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
                // For�a a primeira verifica��o imediatamente ao habilitar (opcional)
                SongTimer_Tick(null, EventArgs.Empty);
                // trayIcon.ShowBalloonTip(1000, "Tidal Now Playing", "Tracking enabled.", ToolTipIcon.Info); // Opcional
            }
            else
            {
                songTimer.Stop();
                _currentSongInfo = string.Empty; // Limpa a m�sica ao desabilitar
                                                 // trayIcon.ShowBalloonTip(1000, "Tidal Now Playing", "Tracking disabled.", ToolTipIcon.Warning); // Opcional
            }

            UpdateTrayToolTip(); // Atualiza o tooltip em ambas as situa��es
        }

        private void UpdateTrayToolTip()
        {
            string status = isEnabled ? "Enabled" : "Disabled";
            string song = string.IsNullOrEmpty(_currentSongInfo) ? "No song playing" : _currentSongInfo;

            // Monta o texto completo
            string toolTipText = $"Tidal Now Playing - Status: {status}";
            if (isEnabled && !string.IsNullOrEmpty(song))
            {
                toolTipText += $" - Playing: {song}";
            }

            // O texto do NotifyIcon tem um limite (geralmente 127 caracteres em vers�es modernas do Windows)
            // Vamos truncar se necess�rio para evitar erros.
            if (toolTipText.Length >= 128)
            {
                toolTipText = toolTipText.Substring(0, 124) + "..."; // Deixa espa�o para "..."
            }

            // Define o texto do tooltip
            if (trayIcon != null) // Garante que trayIcon n�o seja nulo
            {
                trayIcon.Text = toolTipText;
            }
        }

        private void SongTimer_Tick(object? sender, EventArgs e)
        {
            string songInfo = GetTidalWindowTitle();

            // Atualiza a vari�vel interna apenas se a informa��o mudou
            if (_currentSongInfo != songInfo)
            {
                _currentSongInfo = songInfo;
                UpdateTrayToolTip(); // Atualiza o tooltip do �cone
            }


            if (!string.IsNullOrEmpty(songInfo))
            {
                string formatted = $"Current Song: {songInfo}";
                try
                {
                    // Considerar usar um caminho mais robusto como AppData se houver problemas de permiss�o
                    string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TidalNowPlaying");
                    Directory.CreateDirectory(appDataFolder);
                    string filePath = Path.Combine(appDataFolder, "CurrentSongDetails.txt");
                    File.WriteAllText(filePath, formatted);
                }
                catch (Exception ex)
                {
                    // Logar ou tratar erro de escrita no arquivo, se necess�rio
                    Console.WriteLine($"Erro ao escrever arquivo: {ex.Message}");
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
