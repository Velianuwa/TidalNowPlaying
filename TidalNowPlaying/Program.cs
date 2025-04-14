using System.IO;

namespace TidalNowPlaying
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                string errorMessage = $"An fatal error occurred:\n\n" +
                                      $"Message: {ex.Message}\n\n" +
                                      $"Source: {ex.Source}\n\n" +
                                      $"StackTrace:\n{ex.StackTrace}";

                MessageBox.Show(errorMessage, "Error in TidalNowPlaying application", MessageBoxButtons.OK, MessageBoxIcon.Error);

                try
                {
                    string logFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TidalNowPlayingLogs");
                    Directory.CreateDirectory(logFolderPath);
                    string logFilePath = Path.Combine(logFolderPath, "error_log.txt");
                    string logContent = $"{DateTime.Now} - Error:\n{ex.ToString()}\n\n";
                    File.AppendAllText(logFilePath, logContent);
                }
                catch (Exception logEx)
                {
                    MessageBox.Show($"It was not possible to write the error log: {logEx.Message}", "Error writing log", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}