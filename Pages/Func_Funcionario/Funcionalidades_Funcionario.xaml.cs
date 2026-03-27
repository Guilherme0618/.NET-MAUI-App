

namespace GerentProjeto.Pages.Func_Funcionario;

public partial class Funcionalidades_Funcionario : ContentPage
{
	public Funcionalidades_Funcionario()
	{
		InitializeComponent();
	}

	private async void OnAtividadeClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Funcionario_Atividade());
	}

	private async void OnAnotacaoClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Funcionario_Anotacao());
	}

	private async void OnHorasClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Funcionario_Horas());
	}
}