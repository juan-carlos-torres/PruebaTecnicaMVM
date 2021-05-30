using System;
using System.Collections.Generic;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class Correspondencium
    {
        public Correspondencium()
        {
            CorrespondenciaAuditoria = new HashSet<CorrespondenciaAuditorium>();
        }

        public Guid CorId { get; set; }
        public string CorConsecutivo { get; set; }
        public Guid CorIdTipoCorrespondencia { get; set; }
        public Guid CorIdRemitente { get; set; }
        public Guid CorIdDestinatario { get; set; }
        public string CorAsunto { get; set; }
        public string CorDescripcion { get; set; }
        public DateTime CorFechaCreacion { get; set; }

        public virtual Funcionario CorIdDestinatarioNavigation { get; set; }
        public virtual Remitente CorIdRemitenteNavigation { get; set; }
        public virtual TipoCorrespondencium CorIdTipoCorrespondenciaNavigation { get; set; }
        public virtual ICollection<CorrespondenciaAuditorium> CorrespondenciaAuditoria { get; set; }
    }
}
