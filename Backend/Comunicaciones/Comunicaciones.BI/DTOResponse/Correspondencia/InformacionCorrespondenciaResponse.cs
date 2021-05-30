using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunicaciones.BI.DTOResponse.Correspondencia
{
    public class InformacionCorrespondenciaResponse
    {
        public string Consecutivo { get; set; }
        public Guid IdTipoCorrespondencia { get; set; }
        public Guid IdRemitente { get; set; }
        public Guid IdDestinatario { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
    }
}
