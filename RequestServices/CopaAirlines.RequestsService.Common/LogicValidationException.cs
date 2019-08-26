using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.RequestsService.Common
{
    public class LogicValidationException : Exception
    {
        public LogicValidationException()
        {
        }

        public LogicValidationException(string message)
            : base(message)
        {
        }
    }
}
