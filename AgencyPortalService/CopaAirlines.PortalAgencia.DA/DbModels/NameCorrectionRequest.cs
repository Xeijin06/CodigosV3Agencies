using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaAirlines.PortalAgencia.DA.DbModels
{
    public partial class NameCorrectionRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(8)]
        public string PNR { get; set; }
        [Required]
        [StringLength(3)]
        public string Pais { get; set; }
        [Required]
        [StringLength(20)]
        public string ContactPhoneNumber { get; set; }
        [Required]
        [StringLength(15)]
        public string AgenciaID { get; set; }
        [Required]
        [StringLength(20)]
        public string PIN { get; set; }
        [Required]
        [StringLength(1000)]
        public string DescripcionCorreccion { get; set; }
        [Required]
        [StringLength(15)]
        public string IdiomaAtencion { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
