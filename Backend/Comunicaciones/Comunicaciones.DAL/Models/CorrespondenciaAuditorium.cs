using System;
using System.Collections.Generic;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class CorrespondenciaAuditorium
    {
        public Guid CauId { get; set; }
        public Guid CauIdCorrespondencia { get; set; }
        public string CauAsunto { get; set; }
        public string CauDescripcion { get; set; }
        public DateTime CauFechaRegistro { get; set; }

        public virtual Correspondencium CauIdCorrespondenciaNavigation { get; set; }
    }
}
