using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.RequestsService.ViewModel.ApiViewModel
{
    public class AgencyViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IATA { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
