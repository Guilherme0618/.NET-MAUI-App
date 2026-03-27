using GerentProjeto.Pages.Func_Funcionario;
using Microsoft.Maui.Controls;
using System;
using GerentProjeto.Services;

#if WINDOWS
using CommunityToolkit.WinUI.Notifications;
#endif

namespace GerentProjeto.Pages;

public partial class funcionario_login : ContentPage
{
    public funcionario_login()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string nome = userEntry.Text?.Trim();
        string senha = senhaEntry.Text;

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(senha))
        {
            mensagemLabel.Text = "Preencha todos os campos.";
            mensagemLabel.IsVisible = true;
            return;
        }
        // LOGIN ESPECIAL
        if (nome == "Pedro" && senha == "12345")
        {
            await Navigation.PushAsync(new Funcionalidades_Funcionario());

#if WINDOWS
            new ToastContentBuilder()
                .AddText("Acesso Permitido")
                .AddText("Login efetuado como Pedro")
                .Show();
#else
    await DisplayAlert("Acesso Permitido", "Login efetuado como Pedro.", "OK");
#endif

            return;
        }

        var funcionario = await SupabaseService.ObterFuncionarioPorNome(nome);

        if (funcionario != null && funcionario.Senha == senha && funcionario.Funcao == "Funcionario")
        {
            await Navigation.PushAsync(new Funcionalidades_Funcionario());

#if WINDOWS
            new ToastContentBuilder()
                .AddText("Acesso Permitido")
                .AddText($"Login efetuado como {nome}")
                .AddAudio(new Uri("ms-winsoundevent:Notification.Looping.Call"))
                .SetToastScenario(ToastScenario.Alarm)
                .Show();
#else
        await DisplayAlert("Acesso Permitido", $"Login efetuado como {nome}.", "OK");
#endif
        }
        else
        {
            mensagemLabel.Text = "Nome ou senha inválidos, ou acesso não permitido.";
            mensagemLabel.IsVisible = true;
        }
    }

}

