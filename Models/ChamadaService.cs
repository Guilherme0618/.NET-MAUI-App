using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerentProjeto.Models
{
    public static class ChamadaService
    {
        public static ObservableCollection<ChamadaInfo> Chamadas { get; private set; } = new();

        public static void AdicionarChamada(string nome)
        {
            Chamadas.Add(new ChamadaInfo
            {
                NomeUsuario = nome,
                HoraChamada = DateTime.Now
            });
        }
    }
}
