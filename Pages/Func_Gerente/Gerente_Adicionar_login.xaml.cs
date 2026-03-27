using System;
using GerentProjeto.Models;
using GerentProjeto.Services;
using Microsoft.Maui.Controls;

namespace GerentProjeto.Pages.Func_Gerente
{
    public partial class Gerente_Adicionar_login : ContentPage
    {
        private string funcaoSelecionada;

        public Gerente_Adicionar_login()
        {
            InitializeComponent();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnFuncaoCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var radioButton = sender as RadioButton;
                funcaoSelecionada = radioButton?.Value?.ToString();
            }
        }

        private async void OnCadastrarClicked(object sender, EventArgs e)
        {
            string nome = NomeEntry.Text;
            string email = EmailEntry.Text;
            string cpf = CpfEntry.Text;
            string senha = SenhaEntry.Text;

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(cpf) || string.IsNullOrWhiteSpace(senha) ||
                string.IsNullOrWhiteSpace(funcaoSelecionada))
            {
                await DisplayAlert("Erro", "Preencha todos os campos e selecione uma função.", "OK");
                return;
            }

            var funcionario = new Funcionario
            {
                Nome = nome,
                Email = email,
                Cpf = cpf,
                Senha = senha,
                Funcao = funcaoSelecionada
            };

            bool sucesso = await SupabaseService.CadastrarFuncionario(funcionario);
   

            if (sucesso)
            {
                await DisplayAlert("Sucesso", "Cadastro realizado com sucesso!", "OK");

                NomeEntry.Text = "";
                EmailEntry.Text = "";
                CpfEntry.Text = "";
                SenhaEntry.Text = "";
                funcaoSelecionada = "";

                FuncionarioRadioButton.IsChecked = false;
                GerenteRadioButton.IsChecked = false;
            }
            else
            {
                await DisplayAlert("Erro", "Erro ao salvar no banco Supabase.", "OK");
            }
        }
    }
}
