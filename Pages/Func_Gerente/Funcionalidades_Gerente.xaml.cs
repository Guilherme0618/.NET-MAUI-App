
namespace GerentProjeto.Pages.Func_Gerente;

public partial class Funcionalidades_Gerente : ContentPage
{
	public Funcionalidades_Gerente()
	{
		InitializeComponent();
	}

	private async void OnAdicionarClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Gerente_Adicionar_login());
	}

	private async void OnVizualizarClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Gerente_VisualizarFuncionario());
	}
    private async void OnProjetosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Gerente_Projetos());
    }

    private async void OnChamadasClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Gerente_Chamadas());
    }
}
