using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.RequestsService.Common
{
    public class Logger
    {
        public static void CreateLog(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }
    }
}
