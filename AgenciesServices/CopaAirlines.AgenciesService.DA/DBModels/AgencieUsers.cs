using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CopaAirlines.AgenciesService.DA.DBModels
{
    public class AgencieUsers
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public int AgencyID { get; set; }
        [Required]
        public Agencies Agency { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
