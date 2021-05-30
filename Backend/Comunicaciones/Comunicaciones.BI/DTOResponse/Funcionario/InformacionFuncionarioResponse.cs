using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunicaciones.BI.DTOResponse.Funcionario
{
    public class InformacionFuncionarioResponse
    {
        public string Nombres { get; set; }
        public Guid IdRol { get; set; }
        public string Apellidos { get; set; }
        public string Identificacion { get; set; }
    }
}
