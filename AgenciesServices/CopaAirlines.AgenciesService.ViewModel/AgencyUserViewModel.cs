using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.AgenciesService.ViewModel
{
    public class AgencyUserViewModel
    {
        public int Id { get; set; }

        public AgencyViewModel Agency { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
