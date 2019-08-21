using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaAirlines.PortalAgencia.DA.DbModels
{
    public class SecurityAttempt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(150)]
        public string Pin { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public bool? WasAttemptSuccessful { get; set; }

        public bool? IsAgencyBlocked { get; set; }

        public DateTime? BlockedExpirationDateTime { get; set; }
    }
}
