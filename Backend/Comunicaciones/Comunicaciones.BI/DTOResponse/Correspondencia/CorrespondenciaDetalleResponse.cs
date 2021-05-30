using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunicaciones.BI.DTOResponse.Correspondencia
{
    public class CorrespondenciaDetalleResponse
    {
        public Guid Id { get; set; }
        public string Consecutivo { get; set; }
        public string TipoCorrespondencia { get; set; }
        public string NombresRemitente { get; set; }
        public string NombresDestinatario { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public string FechaCreacion { get; set; }
    }
}
