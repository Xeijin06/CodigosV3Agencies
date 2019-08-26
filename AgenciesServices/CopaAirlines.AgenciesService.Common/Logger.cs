using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.AgenciesService.Common
{
    public class Logger
    {
        public static void CreateLog(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }
    }
}
