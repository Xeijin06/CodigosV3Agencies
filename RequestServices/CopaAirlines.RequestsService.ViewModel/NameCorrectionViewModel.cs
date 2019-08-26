using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.RequestsService.ViewModel
{
    public class NameCorrectionViewModel
    {
        public long Id { get; set; }
        
        public int AgencyID { get; set; }

        public string UserEmail { get; set; }

        public int PNR { get; set; }
        
        public string CountryCode { get; set; }
        
        public string ServiceLanguage { get; set; }
        
        public string ContactPhoneNumber { get; set; }
        
        public string Description { get; set; }
        
        public DateTime RegisteredDate { get; set; }
    }
}
