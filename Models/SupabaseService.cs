using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GerentProjeto.Models;

namespace GerentProjeto.Services
{
    public static class SupabaseService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string SUPABASE_URL = "https://lwyqtiufhlgpcomwdhps.supabase.co"; // 🔁 Seu URL Supabase
        private const string API_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imx3eXF0aXVmaGxncGNvbXdkaHBzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTAwMDgxMTksImV4cCI6MjA2NTU4NDExOX0.6qUdXNQesAi_VVnk3eJknlBfSW-m42-DftufCJ58e_Y"; // 🔁 Sua chave anônima

        static SupabaseService()
        {
            _httpClient.BaseAddress = new Uri(SUPABASE_URL);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apikey", API_KEY);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_KEY);
        }

        // ===================== FUNCIONÁRIOS (Tabela Logins) =====================

        public static async Task<bool> CadastrarFuncionario(Funcionario funcionario)
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            var json = JsonSerializer.Serialize(funcionario, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Remove("Prefer");
            _httpClient.DefaultRequestHeaders.Add("Prefer", "return=representation");

            var response = await _httpClient.PostAsync("/rest/v1/Logins", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Erro ao salvar funcionário no Supabase: {response.StatusCode} - {errorContent}");
                return false;
            }

            return true;
        }

        public static async Task<List<Funcionario>> ObterFuncionarios()
        {
            var response = await _httpClient.GetAsync("/rest/v1/Logins");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Erro ao buscar funcionários: {response.StatusCode} - {errorContent}");
                return new List<Funcionario>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var funcionarios = JsonSerializer.Deserialize<List<Funcionario>>(json);

            return funcionarios ?? new List<Funcionario>();
        }

        public static async Task<Funcionario?> ObterFuncionarioPorNome(string nome)
        {
            var response = await _httpClient.GetAsync($"/rest/v1/Logins?Nome=eq.{nome}&select=*");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Erro ao buscar funcionário por nome: {error}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var funcionarios = JsonSerializer.Deserialize<List<Funcionario>>(json);
            return funcionarios?.Count > 0 ? funcionarios[0] : null;
        }

        // ===================== PROJETOS (Tabela Projetos) =====================

        public static async Task<bool> CadastrarProjeto(Projeto projeto)
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            var json = JsonSerializer.Serialize(projeto, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Remove("Prefer");
            _httpClient.DefaultRequestHeaders.Add("Prefer", "return=representation");

            var response = await _httpClient.PostAsync("/rest/v1/Projetos", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"❌ Erro Supabase: {response.StatusCode} - {errorContent}");
                return false;
            }


            return true;
        }


        public static async Task<List<Projeto>> ObterProjetos()
        {
            var response = await _httpClient.GetAsync("/rest/v1/Projetos");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Erro ao buscar projetos: {response.StatusCode} - {errorContent}");
                return new List<Projeto>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var projetos = JsonSerializer.Deserialize<List<Projeto>>(json);

            return projetos ?? new List<Projeto>();
        }
    }
}
