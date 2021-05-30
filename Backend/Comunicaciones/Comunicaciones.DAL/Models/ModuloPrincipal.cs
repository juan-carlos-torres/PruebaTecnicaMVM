using System;
using System.Collections.Generic;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class ModuloPrincipal
    {
        public ModuloPrincipal()
        {
            Modulos = new HashSet<Modulo>();
        }

        public Guid MprId { get; set; }
        public string MprNombre { get; set; }
        public bool MprActivo { get; set; }

        public virtual ICollection<Modulo> Modulos { get; set; }
    }
}
