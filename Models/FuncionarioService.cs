using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerentProjeto.Models
{
    public sealed class FuncionarioService
    {
        private static readonly FuncionarioService _instance = new FuncionarioService();
        public static FuncionarioService Instance => _instance;

        private readonly List<Funcionario> _funcionarios = new List<Funcionario>();

        // Construtor privado para evitar instância externa
        private FuncionarioService() { }

        public void AdicionarFuncionario(Funcionario funcionario)
        {
            _funcionarios.Add(funcionario);
        }

        public List<Funcionario> ObterFuncionarios()
        {
            return _funcionarios;
        }
    }
}
