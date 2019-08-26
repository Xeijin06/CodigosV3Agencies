using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CopaAirlines.RequestsService.DA.DbModel
{
    public class NameCorrectionRequest
    {
        [Key]
        public long NameCorrectionRequestID { get; set; }

        [Required]
        public int AgencyID { get; set; }

        [Required]
        public int PNR { get; set; }

        [Required]
        [StringLength(2)]
        public string CountryCode { get; set; }

        [Required]
        [StringLength(2)]
        public string ServiceLanguage { get; set; }

        [Required]
        [StringLength(20)]
        public string ContactPhoneNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public DateTime RegisteredDate { get; set; }
    }
}
