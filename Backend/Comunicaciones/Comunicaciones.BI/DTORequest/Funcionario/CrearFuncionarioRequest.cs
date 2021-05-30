using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunicaciones.BI.DTORequest.Funcionario
{
    public class CrearFuncionarioRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} puede tener máximo {1} caracteres")]
        public string Nombres { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} puede tener máximo {1} caracteres")]
        public string Apellidos { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(20, ErrorMessage = "El campo {0} puede tener máximo {1} caracteres")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid IdRol { get; set; }
    }
}
