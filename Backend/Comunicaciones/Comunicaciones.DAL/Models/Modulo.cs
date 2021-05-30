using System;
using System.Collections.Generic;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class Modulo
    {
        public Modulo()
        {
            RolModulos = new HashSet<RolModulo>();
        }

        public Guid ModId { get; set; }
        public Guid ModIdModuloPrincipal { get; set; }
        public string ModNombre { get; set; }
        public bool ModActivo { get; set; }

        public virtual ModuloPrincipal ModIdModuloPrincipalNavigation { get; set; }
        public virtual ICollection<RolModulo> RolModulos { get; set; }
    }
}
