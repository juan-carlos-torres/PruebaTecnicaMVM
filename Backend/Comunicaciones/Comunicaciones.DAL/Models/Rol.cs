using System;
using System.Collections.Generic;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Funcionarios = new HashSet<Funcionario>();
            RolModulos = new HashSet<RolModulo>();
        }

        public Guid RolId { get; set; }
        public string RolNombre { get; set; }
        public bool RolActivo { get; set; }

        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        public virtual ICollection<RolModulo> RolModulos { get; set; }
    }
}
