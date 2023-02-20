using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Core.Interface.Helper
{
    public interface ICryptoHelper
    {
        string Encrypt(string input);
        string Decrypt(string input);

    }
}
