using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Model
{
    public class AppSettings
    {
        public Connectionstrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Apimodel ApiModel { get; set; }
        public PaymentModel PaymentModel { get; set; }

    }

    public class Connectionstrings
    {
        public string DefaultConnection { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class Apimodel
    {
        public string ContentType { get; set; }
        public string Accept { get; set; }
        public string XIBMClientId { get; set; }
        public string XIBMClientSecret { get; set; }
        public string TokenUrl { get; set; }
        public string CreateOrderUrl { get; set; }
        public string CreateBarkode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class PaymentModel
    {
        public Ipara Ipara { get; set; }
    }
    public class Ipara
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string BaseUrl { get; set; }
        public string Version { get; set; }
        public string Mode { get; set; }
        public string HashString { get; set; }
    }
}

