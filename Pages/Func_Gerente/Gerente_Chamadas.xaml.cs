using GerentProjeto.Models;

namespace GerentProjeto.Pages.Func_Gerente;

public partial class Gerente_Chamadas : ContentPage
{
    public Gerente_Chamadas()
    {
        InitializeComponent();
        ChamadasListView.ItemsSource = ChamadaService.Chamadas;
    }

    private async void OnVoltarClicked(Object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Funcionalidades_Gerente());
    }
}
