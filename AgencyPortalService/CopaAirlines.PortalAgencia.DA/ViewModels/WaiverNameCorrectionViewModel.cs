using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaAirlines.PortalAgencia.DA.ViewModels
{
    public class WaiverNameCorrectionViewModel
    {
        public string PNR { get; set; }
        public string CountryCode { get; set; }
        //public string PIN { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
        public string ServiceLanguage { get; set; }
        public string ContactPhoneNumber { get; set; }
    }
}