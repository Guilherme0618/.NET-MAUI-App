using System;
using System.Collections.ObjectModel;
using GerentProjeto.Models;
using GerentProjeto.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;

namespace GerentProjeto.Pages.Func_Gerente
{
    public partial class Gerente_Projetos : ContentPage
    {
        public ObservableCollection<Projeto> Projetos { get; set; } = new ObservableCollection<Projeto>();

        public Gerente_Projetos()
        {
            InitializeComponent();
            BindingContext = this;

            CarregarProjetosAsync();
        }

        private async void CarregarProjetosAsync()
        {
            var lista = await SupabaseService.ObterProjetos();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Projetos.Clear();
                foreach (var projeto in lista)
                {
                    Projetos.Add(projeto);
                }
            });
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Funcionalidades_Gerente());
        }

        private async void OnCriarProjetoClicked(object sender, EventArgs e)
        {
            string nome = await DisplayPromptAsync("Novo Projeto", "Digite o nome do projeto:");
            if (string.IsNullOrWhiteSpace(nome)) return;

            string responsavel = await DisplayPromptAsync("Novo Projeto", "Digite o nome do responsável:");
            if (string.IsNullOrWhiteSpace(responsavel)) return;

            string duracao = await DisplayPromptAsync("Novo Projeto", "Digite a duração do projeto (ex: 3 meses):");
            if (string.IsNullOrWhiteSpace(duracao)) return;

            string resumo = await DisplayPromptAsync("Novo Projeto", "Digite o resumo do projeto:");
            if (string.IsNullOrWhiteSpace(resumo)) return;

            var novoProjeto = new Projeto
            {
                Nome = nome,
                Responsavel = responsavel,
                Duracao = duracao,
                Resumo = resumo
            };

            // Para adicionar projeto:
            bool sucesso = await SupabaseService.CadastrarProjeto(novoProjeto);
            if (sucesso)
            {
                Projetos.Add(novoProjeto);
                await DisplayAlert("Sucesso", "Projeto criado com sucesso!", "OK");
            }
            else
            {
                await DisplayAlert("Erro", "Falha ao salvar o projeto no banco de dados. Veja detalhes no debug.", "OK");
            }


        }
    }
}
