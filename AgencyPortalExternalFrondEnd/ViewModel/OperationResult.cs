using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class OperationResult
    {
        public bool Result { get; set; } = false;
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
