# TidalNowPlaying

🇧🇷 [Português](#português) | 🇺🇸 [English](#english)

---

## 🇧🇷 Português

Um aplicativo simples para Windows que roda na bandeja do sistema (system tray), lê a música que está tocando no aplicativo Tidal Desktop e a exibe no tooltip do ícone, além de salvá-la em um arquivo de texto.

### ✨ Funcionalidades

* Executa minimizado na bandeja do sistema.
* Mostra o título da música atual do Tidal e o status do aplicativo (Habilitado/Desabilitado) ao passar o mouse sobre o ícone na bandeja.
* Interface simples com botão para Habilitar/Desabilitar o monitoramento.
* Opção para minimizar a janela principal para a bandeja do sistema.
* Salva o título da música atual (`Artista - Título`) em um arquivo de texto, útil para overlays de streaming (Ex: OBS, Streamlabs).

### 🚀 Download & Instalação

#### Pré-requisitos

* **Sistema Operacional Windows** (Testado no Windows 10/11)
* **[.NET 8 Desktop Runtime (x64)](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)**
    * ⚠️ **Importante:** Certifique-se de baixar e instalar o **"Desktop Runtime"** (Runtime de Área de Trabalho) para **x64**. Não instale apenas o ".NET Runtime" base ou o SDK.

#### Executável Pronto (Release)

1.  Vá para a **[Página de Releases](https://github.com/MiguelMachado-dev/TidalNowPlaying/releases)** aqui no GitHub.
2.  Na seção **Assets** da release mais recente (latest), baixe o arquivo `TidalNowPlaying.exe` (ou um arquivo `.zip` que o contenha).
3.  Salve o `.exe` em qualquer pasta do seu computador.
4.  Execute o `TidalNowPlaying.exe`.

### ⚙️ Como Usar

1.  Execute o arquivo `TidalNowPlaying.exe`.
2.  A janela do aplicativo aparecerá. Clique em **"Enable"** para começar a monitorar o Tidal.
3.  O ícone do aplicativo aparecerá na bandeja do sistema (perto do relógio). Passe o mouse sobre ele para ver o status e a música atual.
4.  Marque a caixa **"Minimize to tray"** se quiser que a janela principal se esconda ao ser minimizada.
5.  A informação da música atual é salva automaticamente no arquivo:
    `%AppData%\TidalNowPlaying\CurrentSongDetails.txt`
    * *(Dica: Cole `%AppData%\TidalNowPlaying` na barra de endereço do Windows Explorer para abrir a pasta)*.
    * Este arquivo pode ser usado como fonte de texto em softwares como OBS, Streamlabs, etc., para mostrar a música na sua live.
6.  Clique em **"Disable"** para parar o monitoramento. Para fechar completamente o aplicativo, clique com o botão direito no ícone na bandeja e selecione **"Exit"**.

### 🛠️ Compilando do Código Fonte (Opcional)

Se preferir compilar o aplicativo você mesmo:

1.  Instale o **[.NET 8 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)** completo.
2.  Clone o repositório:
    ```bash
    git clone [https://github.com/MiguelMachado-dev/TidalNowPlaying.git](https://github.com/MiguelMachado-dev/TidalNowPlaying.git)
    ```
3.  Navegue até a pasta do repositório:
    ```bash
    cd TidalNowPlaying
    ```
4.  Compile o projeto (versão dependente de framework):
    ```bash
    # Navegue para a pasta do projeto primeiro
    cd TidalNowPlaying
    dotnet build -c Release
    ```
    *(Ou abra o arquivo `TidalNowPlaying.sln` no Visual Studio 2022 ou mais recente e compile por lá)*.
5.  Para gerar o executável único otimizado (como o da release), use o comando publish:
    ```bash
    dotnet publish -c Release -r win-x64 --self-contained false /p:PublishSingleFile=true /p:PublishReadyToRun=true
    ```
    O resultado estará em `bin/Release/net8.0-windows/win-x64/publish/`.

### 🔒 Verificação de Integridade e Autenticidade

Você pode verificar se o arquivo `.exe` baixado é autêntico e não foi corrompido:

#### ✔️ Passo 1: Baixe os arquivos

Além do `TidalNowPlaying.exe`, baixe também os arquivos:

- `TidalNowPlaying.exe.sha256`
- `TidalNowPlaying.exe.sha256.asc`
- `miguelmachado-pubkey.asc`

#### ✔️ Passo 2: Instale o GPG

Caso ainda não tenha, instale o [Gpg4win](https://gpg4win.org/) para usar o GPG no Windows.

#### ✔️ Passo 3: Importe a chave pública

```powershell
gpg --import .\miguelmachado-pubkey.asc
```

#### ✔️ Passo 4: Verifique a assinatura da hash

```powershell
gpg --verify .\TidalNowPlaying.exe.sha256.asc .\TidalNowPlaying.exe.sha256
```

A saída deve conter:

```
gpg: Boa assinatura de "Miguel Machado <hello@miguelmachado.dev>"
```

#### ✔️ Passo 5: Verifique o hash do executável

```powershell
Get-FileHash .\TidalNowPlaying.exe -Algorithm SHA256
```

Compare com o conteúdo de `TidalNowPlaying.exe.sha256`. Os valores devem ser idênticos.

### 📄 Licença

Este projeto é distribuído sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 🇺🇸 English

A simple Windows application that runs in the system tray, reads the currently playing song from the Tidal Desktop app, displays it in the icon tooltip, and saves it to a text file.

### ✨ Features

* Runs minimized in the system tray.
* Displays the current Tidal song title and app status (Enabled/Disabled) in the tray icon tooltip on hover.
* Simple UI with a button to Enable/Disable tracking.
* Option to minimize the main window to the system tray.
* Saves the current song title (`Artist - Title`) to a text file, useful for streaming overlays (e.g., OBS, Streamlabs).

### 🚀 Download & Installation

#### Prerequisites

* **Windows Operating System** (Tested on Windows 10/11)
* **[.NET 8 Desktop Runtime (x64)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**
    * ⚠️ **Important:** Make sure to download and install the **"Desktop Runtime"** for **x64**. Do not install just the base ".NET Runtime" or the SDK.

#### Ready-to-use Executable (Release)

1.  Go to the **[Releases Page](https://github.com/MiguelMachado-dev/TidalNowPlaying/releases)** here on GitHub.
2.  In the **Assets** section of the latest release, download the `TidalNowPlaying.exe` file (or a `.zip` file containing it).
3.  Save the `.exe` file to any folder on your computer.
4.  Run `TidalNowPlaying.exe`.

### ⚙️ How to Use

1.  Run the `TidalNowPlaying.exe` file.
2.  The application window will appear. Click **"Enable"** to start monitoring Tidal.
3.  The application icon will appear in the system tray (near the clock). Hover over it to see the status and the current song.
4.  Check the **"Minimize to tray"** box if you want the main window to hide when minimized.
5.  The current song information is automatically saved to the file:
    `%AppData%\TidalNowPlaying\CurrentSongDetails.txt`
    * *(Tip: Paste `%AppData%\TidalNowPlaying` into the Windows Explorer address bar to open the folder)*.
    * This file can be used as a text source in software like OBS, Streamlabs, etc., to display the current song on your stream.
6.  Click **"Disable"** to stop monitoring. To close the application completely, right-click the tray icon and select **"Exit"**.

### 🛠️ Building from Source (Optional)

If you prefer to build the application yourself:

1.  Install the full **[.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**.
2.  Clone the repository:
    ```bash
    git clone [https://github.com/MiguelMachado-dev/TidalNowPlaying.git](https://github.com/MiguelMachado-dev/TidalNowPlaying.git)
    ```
3.  Navigate to the repository folder:
    ```bash
    cd TidalNowPlaying
    ```
4.  Build the project (framework-dependent version):
    ```bash
    # Navigate to the project folder first
    cd TidalNowPlaying
    dotnet build -c Release
    ```
    *(Or open the `TidalNowPlaying.sln` file in Visual Studio 2022 or later and build it)*.
5.  To generate the optimized single-file executable (like the one in the release), use the publish command:
    ```bash
    dotnet publish -c Release -r win-x64 --self-contained false /p:PublishSingleFile=true /p:PublishReadyToRun=true
    ```
    The result will be in `bin/Release/net8.0-windows/win-x64/publish/`.

### 🔒 Integrity and Authenticity Verification

You can verify that the `.exe` file is authentic and has not been tampered with:

#### ✔️ Step 1: Download the verification files

Alongside `TidalNowPlaying.exe`, also download:

- `TidalNowPlaying.exe.sha256`
- `TidalNowPlaying.exe.sha256.asc`
- `miguelmachado-pubkey.asc`

#### ✔️ Step 2: Install GPG

If you don’t have it yet, install [Gpg4win](https://gpg4win.org/) to use GPG on Windows.

#### ✔️ Step 3: Import the public key

```powershell
gpg --import .\miguelmachado-pubkey.asc
```

#### ✔️ Step 4: Verify the signature

```powershell
gpg --verify .\TidalNowPlaying.exe.sha256.asc .\TidalNowPlaying.exe.sha256
```

You should see:

```
gpg: Good signature from "Miguel Machado <hello@miguelmachado.dev>"
```

#### ✔️ Step 5: Verify the `.exe` checksum

```powershell
Get-FileHash .\TidalNowPlaying.exe -Algorithm SHA256
```

Compare the output with the value in `TidalNowPlaying.exe.sha256`. They should match exactly.

### 📄 License

This project is distributed under the MIT License. See the [LICENSE](LICENSE) file for more details.
