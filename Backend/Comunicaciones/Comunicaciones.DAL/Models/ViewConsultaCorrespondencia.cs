using System;
using System.Collections.Generic;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class ViewConsultaCorrespondencia
    {
        public string Consecutivo { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string TipoCorrespondencia { get; set; }
        public string NombreRemitente { get; set; }
        public string NombreDestinatario { get; set; }
    }
}
