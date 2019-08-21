using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaAirlines.PortalAgencia.DA.DbModels
{
    public class Agency
    {
        [Key]
        public int AgencyID { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }


        [StringLength(100)]
        public string User { get; set; }

        [Required]
        [StringLength(10)]
        public string PIN { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(8)]
        public string IATACode { get; set; }
    }
}
