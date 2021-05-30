using System;
using System.Collections.Generic;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class RolModulo
    {
        public Guid RmoIdRol { get; set; }
        public Guid RmoIdModulo { get; set; }

        public virtual Modulo RmoIdModuloNavigation { get; set; }
        public virtual Rol RmoIdRolNavigation { get; set; }
    }
}
