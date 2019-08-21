using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class NameCorrectionViewModel
    {
        [Required(ErrorMessage = "El campo PNR es obligatorio")]
        [StringLength(6)]
        public string PNR { get; set; }
        [Required(ErrorMessage = "El campo Teléfono de contacto es obligatorio")]
        [StringLength(20, ErrorMessage = "El campo de Teléfono de contacto debe ser una cadena con una longitud máxima de 20.")]
        public string ContactPhoneNumber { get; set; }
        public string User { get; set; }
        [Required(ErrorMessage = "El campo de País es obligatorio")]
        [StringLength(2)]
        public string CountryCode { get; set; }
        [Required(ErrorMessage = "El campo Idioma es obligatorio")]
        [StringLength(2)]
        public string ServiceLanguage { get; set; }
        [Required(ErrorMessage = "El campo Descripción es obligatorio")]
        [StringLength(200)]
        public string Description { get; set; }
    }
}
