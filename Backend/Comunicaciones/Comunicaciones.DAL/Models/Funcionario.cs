using System;
using System.Collections.Generic;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class Funcionario
    {
        public Funcionario()
        {
            Correspondencia = new HashSet<Correspondencium>();
        }

        public Guid FunId { get; set; }
        public Guid FunIdRol { get; set; }
        public string FunNombres { get; set; }
        public string FunApellidos { get; set; }
        public string FunIdentificacion { get; set; }
        public bool FunActivo { get; set; }

        public virtual Rol FunIdRolNavigation { get; set; }
        public virtual ICollection<Correspondencium> Correspondencia { get; set; }
    }
}
