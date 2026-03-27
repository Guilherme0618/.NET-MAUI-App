using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerentProjeto.Models
{
    public class ChamadaInfo
    {
        public string NomeUsuario { get; set; }
        public DateTime HoraChamada { get; set; }
        public string HoraFormatada => HoraChamada.ToString("HH:mm:ss");
    }
}

