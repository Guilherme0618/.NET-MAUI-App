using GerentProjeto.Models;
using GerentProjeto.Services;
using System.Collections.ObjectModel;

namespace GerentProjeto.Pages.Func_Gerente
{
    public partial class Gerente_VisualizarFuncionario : ContentPage
    {
        public ObservableCollection<Funcionario> Funcionarios { get; set; } = new();

        public Gerente_VisualizarFuncionario()
        {
            InitializeComponent();
            BindingContext = this;

            CarregarFuncionarios(); // carregar dados do Supabase
        }

        private async void CarregarFuncionarios()
        {
            var lista = await SupabaseService.ObterFuncionarios();

            Funcionarios.Clear();
            foreach (var func in lista)
            {
                Funcionarios.Add(func);
            }
        }

        private async void OnVoltarClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Funcionalidades_Gerente());
        }
    }

}
