using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;

namespace GerentProjeto.Pages.Func_Funcionario
{
    public partial class Funcionario_Horas : ContentPage
    {
        private DateTime? inicioPonto;
        private IDispatcherTimer dispatcherTimer;

        public Funcionario_Horas()
        {
            InitializeComponent();
            DataLabel.Text = $"Data: {DateTime.Today:dd/MM/yyyy}";

            dispatcherTimer = Dispatcher.CreateTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += (s, e) =>
            {
                if (inicioPonto.HasValue)
                {
                    var duracao = DateTime.Now - inicioPonto.Value;
                    TempoDecorridoLabel.Text = $"Tempo decorrido: {duracao:hh\\:mm\\:ss}";
                }
            };
        }

        private void OnIniciarClicked(object sender, EventArgs e)
        {
            inicioPonto = DateTime.Now;
            DisplayAlert("Ponto iniciado", $"Início: {inicioPonto.Value:dd/MM/yyyy HH:mm:ss}", "OK");

            BtnParar.IsEnabled = true;
            BtnIniciar.IsEnabled = false;

            TempoDecorridoLabel.Text = "Tempo decorrido: 00:00:00";
            dispatcherTimer.Start();
        }

        private void OnPararClicked(object sender, EventArgs e)
        {
            if (inicioPonto == null)
            {
                DisplayAlert("Erro", "Você precisa iniciar o ponto antes de parar.", "OK");
                return;
            }

            var fimPonto = DateTime.Now;
            var duracao = fimPonto - inicioPonto.Value;

            var frame = new Frame
            {
                Padding = 10,
                Margin = new Thickness(5),
                BorderColor = Colors.Gray,
                CornerRadius = 10,
                Content = new VerticalStackLayout
                {
                    Children =
                    {
                        new Label { Text = $"Data: {inicioPonto.Value:dd/MM/yyyy}", FontAttributes = FontAttributes.Bold },
                        new Label { Text = $"Início: {inicioPonto.Value:HH:mm:ss}" },
                        new Label { Text = $"Término: {fimPonto:HH:mm:ss}" },
                        new Label { Text = $"Duração: {duracao.Hours}h {duracao.Minutes}m {duracao.Seconds}s" }
                    }
                }
            };

            PontosContainer.Children.Add(frame);

            BtnParar.IsEnabled = false;
            BtnIniciar.IsEnabled = true;

            dispatcherTimer.Stop();
            TempoDecorridoLabel.Text = "Tempo decorrido: 00:00:00";
            inicioPonto = null;
        }
        private async void OnVoltarClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Funcionalidades_Funcionario());
        }
    }
}
