#if WINDOWS
using CommunityToolkit.WinUI.Notifications;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage;
using GerentProjeto.Models;
#endif

namespace GerentProjeto.Pages;

public partial class Call_page : ContentPage
{
    public Call_page()
    {
        InitializeComponent();
    }

    public async void OnCallClicked(object sender, EventArgs e)
    {
        string nomeUsuario = await DisplayPromptAsync("Responsável pela chamada", "Por favor, digite seu usuário antes de chamar um gerente:");

        if (string.IsNullOrWhiteSpace(nomeUsuario))
        {
            await DisplayAlert("Aviso", "Você precisa informar seu nome para continuar.", "OK");
            return;
        }
        ChamadaService.AdicionarChamada(nomeUsuario);

#if WINDOWS
        string path = Path.Combine(AppContext.BaseDirectory, "som_notificacao.mp3");
        var mediaPlayer = new MediaPlayer();
        mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(path));
        mediaPlayer.Play();

        new ToastContentBuilder()
            .AddText("Chamado Enviado")
            .AddText($"{nomeUsuario}. Um gerente foi acionado e responderá em breve!")
            .Show();
#else
await DisplayAlert("Chamado Enviado", $"{nomeUsuario}. Um gerente foi acionado e responderá em breve!", "OK");
#endif

    }
}
