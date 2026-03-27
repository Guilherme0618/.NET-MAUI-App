namespace GerentProjeto.Pages.Func_Funcionario;

public partial class Funcionario_Atividade : ContentPage
{
    private int contador = 1;
    private Frame atividadeSelecionada;
    private Color corSelecionada;

    public Funcionario_Atividade()
    {
        InitializeComponent();

        BtnCorVerde.Clicked += (s, e) => SelecionarCor(Colors.LightGreen);
        BtnCorAmarelo.Clicked += (s, e) => SelecionarCor(Colors.LightYellow);
        BtnCorVermelho.Clicked += (s, e) => SelecionarCor(Colors.LightCoral);
    }

    private void SelecionarCor(Color cor)
    {
        corSelecionada = cor;

       
        BtnCorVerde.BorderColor = Colors.Transparent;
        BtnCorAmarelo.BorderColor = Colors.Transparent;
        BtnCorVermelho.BorderColor = Colors.Transparent;

       
        if (cor == Colors.LightGreen)
            BtnCorVerde.BorderColor = Colors.Black;
        else if (cor == Colors.LightYellow)
            BtnCorAmarelo.BorderColor = Colors.Black;
        else if (cor == Colors.LightCoral)
            BtnCorVermelho.BorderColor = Colors.Black;
    }

    public class AtividadeInfo
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Color Cor { get; set; }
    }

    private Frame CriarAtividade(int numero, string nome = null, string descricao = null, Color corFundo = null)
    {
        nome ??= $"{numero}";
        descricao ??= "";
        corFundo ??= Colors.LightGreen;

        var info = new AtividadeInfo
        {
            Nome = nome,
            Descricao = descricao,
            Cor = corFundo
        };

        var labelNome = new Label
        {
            Text = info.Nome,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold,
            FontSize = 14,
            TextColor = Colors.Black
        };

        var frameAtividade = new Frame
        {
            WidthRequest = 150,
            HeightRequest = 100,
            BackgroundColor = info.Cor,
            CornerRadius = 12,
            Padding = 10,
            Margin = new Thickness(5),
            Content = labelNome,
            BindingContext = info
        };

        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (s, args) =>
        {
            atividadeSelecionada = frameAtividade;

            if (frameAtividade.BindingContext is AtividadeInfo infoSelecionada)
            {
                DescricaoFrame.IsVisible = true;
                NomeEntry.Text = infoSelecionada.Nome;
                DescricaoEditor.Text = infoSelecionada.Descricao;
                SelecionarCor(infoSelecionada.Cor);
            }
        };
        frameAtividade.GestureRecognizers.Add(tapGesture);

        return frameAtividade;
    }

    private void OnAdicionarAtividadeClicked(object sender, EventArgs e)
    {
        if (AtividadesContainer.Children.Count >= 20)
        {
            DisplayAlert("Limite atingido", "Você não pode adicionar mais que 20 atividades.", "OK", "Cancelar");
            return;
        }

        var atividade = CriarAtividade(contador);
        AtividadesContainer.Children.Add(atividade);
        contador++;
    }

    private void OnSalvarClicked(object sender, EventArgs e)
    {
        if (atividadeSelecionada == null)
            return;

        if (atividadeSelecionada.BindingContext is AtividadeInfo info && atividadeSelecionada.Content is Label label)
        {
            info.Nome = NomeEntry.Text;
            info.Descricao = DescricaoEditor.Text;
            info.Cor = corSelecionada;

            label.Text = info.Nome;
            atividadeSelecionada.BackgroundColor = info.Cor;
        }

        DescricaoFrame.IsVisible = false;
    }

    private void OnExcluirClicked(object sender, EventArgs e)
    {
        if (atividadeSelecionada != null)
        {
            // Remove do container
            AtividadesContainer.Children.Remove(atividadeSelecionada);

            // Limpa seleção
            atividadeSelecionada = null;

            // Esconde o painel de descrição
            DescricaoFrame.IsVisible = false;

            // Opcional: limpa os campos da descrição
            NomeEntry.Text = string.Empty;
            DescricaoEditor.Text = string.Empty;
            corSelecionada = Colors.Transparent;

            // Remove seleção visual dos botões de cor
            BtnCorVerde.BorderColor = Colors.Transparent;
            BtnCorAmarelo.BorderColor = Colors.Transparent;
            BtnCorVermelho.BorderColor = Colors.Transparent;
        }
    }

    private async void OnVoltarClicked(Object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Funcionalidades_Funcionario());
    }

}
