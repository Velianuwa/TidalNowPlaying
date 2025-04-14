using System.IO; // Adicione esta linha se for usar o log em arquivo

namespace TidalNowPlaying
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Configurações da aplicação
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Bloco try-catch para capturar erros na inicialização
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                // Mostra o erro detalhado para o usuário
                string errorMessage = $"Ocorreu um erro fatal:\n\n" +
                                      $"Mensagem: {ex.Message}\n\n" +
                                      $"Origem: {ex.Source}\n\n" +
                                      $"StackTrace:\n{ex.StackTrace}";

                MessageBox.Show(errorMessage, "Erro na Aplicação TidalNowPlaying", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // (Opcional, mas recomendado) Logar o erro em um arquivo para análise posterior
                try
                {
                    string logFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TidalNowPlayingLogs");
                    Directory.CreateDirectory(logFolderPath); // Cria a pasta se não existir
                    string logFilePath = Path.Combine(logFolderPath, "error_log.txt");
                    string logContent = $"{DateTime.Now} - Erro:\n{ex.ToString()}\n\n";
                    File.AppendAllText(logFilePath, logContent);
                }
                catch (Exception logEx)
                {
                    // Se até o log falhar, mostra um erro adicional simples
                    MessageBox.Show($"Não foi possível gravar o log de erro: {logEx.Message}", "Erro de Log", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}