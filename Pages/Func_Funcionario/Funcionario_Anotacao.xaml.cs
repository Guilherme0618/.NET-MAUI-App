namespace GerentProjeto.Pages.Func_Funcionario;

public partial class Funcionario_Anotacao : ContentPage
{
	public Funcionario_Anotacao()
	{
		InitializeComponent();
	}

    private async void OnVoltarClicked(Object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Funcionalidades_Funcionario());
    }
}