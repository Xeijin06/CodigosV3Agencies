using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.RequestsService.ViewModel
{
    public class EmailViewModel
    {
        public string SMTPServer { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
