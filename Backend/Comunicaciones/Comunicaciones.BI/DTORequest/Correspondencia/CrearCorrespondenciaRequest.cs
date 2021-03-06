using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunicaciones.BI.DTORequest.Correspondencia
{
    public class CrearCorrespondenciaRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid IdTipoCorrespondencia { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid IdRemitente { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid IdDestinatario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} puede tener máximo {1} caracteres")]
        public string Asunto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(200, ErrorMessage = "El campo {0} puede tener máximo {1} caracteres")]
        public string Descripcion { get; set; }
    }
}
