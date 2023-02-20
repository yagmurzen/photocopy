using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Helper
{
    public static class ConverterHelper
    {

        public static string Base64ToImage(IFormFile file)
        {
            string str = "";
            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    str = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }

            return str;
        }
    }
}
