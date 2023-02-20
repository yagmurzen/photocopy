using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Photocopy.Core.Interface.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Helper
{
    
    public class CookieHelper:ICookieHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICryptoHelper _cryptoHelper;


        public CookieHelper(IHttpContextAccessor httpContextAccessor, ICryptoHelper cryptoHelper)
        {
            _httpContextAccessor = httpContextAccessor;
            _cryptoHelper = cryptoHelper;
        }

        public dynamic GetCookie(string key)
        {
             _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(key, out string value); ;
            
            return !String.IsNullOrEmpty(value) ? _cryptoHelper.Decrypt(value):"";       
        }

        public void SetCookie(string key,dynamic value)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            options.IsEssential = true;
            string cryptoStr= _cryptoHelper.Encrypt(Convert.ToString(value));
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, cryptoStr,options);

        }

        public void RemoveCookie(string key)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(-1);
            options.IsEssential = true;
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key,"", options);
        }
    }
}
