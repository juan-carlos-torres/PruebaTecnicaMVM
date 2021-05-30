using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunicaciones.BI.DTORequest.Remitente
{
    public class EditarRemitenteRequest
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
    }
}
