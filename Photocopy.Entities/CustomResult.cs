using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities
{
    public class ErrorResult
    {
        public ErrorResult() { }
        public ErrorResult(string message) { }
        public string Message { get; set; }
       
    }
}
