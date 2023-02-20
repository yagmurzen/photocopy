using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Core.Interface.Helper
{
    public interface ICookieHelper
    {
        dynamic GetCookie(string key);
        void SetCookie(string key, dynamic value);
        void RemoveCookie(string key);
    }
}
