using System;
using System.Collections.Generic;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class RemitenteAuditorium
    {
        public Guid RauId { get; set; }
        public Guid RauIdRemitente { get; set; }
        public string RauNombres { get; set; }
        public string RauApellidos { get; set; }
        public string RauIdentificacion { get; set; }
        public DateTime RauFechaRegistro { get; set; }

        public virtual Remitente RauIdRemitenteNavigation { get; set; }
    }
}
