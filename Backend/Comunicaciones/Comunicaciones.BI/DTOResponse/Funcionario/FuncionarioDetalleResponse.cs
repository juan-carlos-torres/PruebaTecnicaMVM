using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunicaciones.BI.DTOResponse.Funcionario
{
    public class FuncionarioDetalleResponse
    {
        public Guid Id { get; set; }
        public string Rol { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Identificacion { get; set; }
    }
}
