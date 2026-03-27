namespace GerentProjeto
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool confirmarSaida = await DisplayAlert("Confirmação", "Deseja realmente fechar o aplicativo?", "Sim", "Não");

            if (confirmarSaida)
            {
                #if WINDOWS
                                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                #elif MACCATALYST
                                Environment.Exit(0);
                #else
                                    Application.Current.Quit();
                #endif
            }
        }
    }
}