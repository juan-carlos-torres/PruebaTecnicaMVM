using System;
using System.Collections.Generic;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class TipoCorrespondencium
    {
        public TipoCorrespondencium()
        {
            Correspondencia = new HashSet<Correspondencium>();
        }

        public Guid TcoId { get; set; }
        public string TcoNombre { get; set; }
        public string TcoPrefijo { get; set; }
        public int TcoConsecutivo { get; set; }
        public bool TcoActivo { get; set; }

        public virtual ICollection<Correspondencium> Correspondencia { get; set; }
    }
}
