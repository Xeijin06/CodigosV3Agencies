using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CopaAirlines.AgenciesService.DA.DBModels
{
    public class Agencies
    {
        [Key]
        public int AgencyID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(8)]
        public string IATACode { get; set; }
        
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
