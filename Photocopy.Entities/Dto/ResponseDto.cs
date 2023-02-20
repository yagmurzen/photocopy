using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Dto
{
    public class ResponseDto
    {
        public HttpStatusCode statusCode { get; set; }
        public Exception exception {get;set;}

        public string responseMessage { get; set; }
        public dynamic resultData { get; set; }
        public bool result { get; set; }

    }
}
