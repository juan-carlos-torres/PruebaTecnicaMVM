using System;
using System.Collections.Generic;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class Remitente
    {
        public Remitente()
        {
            Correspondencia = new HashSet<Correspondencium>();
            RemitenteAuditoria = new HashSet<RemitenteAuditorium>();
        }

        public Guid RemId { get; set; }
        public string RemNombres { get; set; }
        public string RemApellidos { get; set; }
        public string RemIdentificacion { get; set; }
        public DateTime RemFechaCreacion { get; set; }

        public virtual ICollection<Correspondencium> Correspondencia { get; set; }
        public virtual ICollection<RemitenteAuditorium> RemitenteAuditoria { get; set; }
    }
}
