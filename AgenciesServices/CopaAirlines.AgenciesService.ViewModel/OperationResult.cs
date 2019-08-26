using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.AgenciesService.ViewModel
{
    public class OperationResult
    {
        public bool Result { get; set; } = false;
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
