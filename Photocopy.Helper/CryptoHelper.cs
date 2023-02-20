using Microsoft.AspNetCore.DataProtection;
using Photocopy.Core.Interface.Helper;
using System.Net;

namespace Photocopy.Helper
{
    public class CryptoHelper : ICryptoHelper
    {
		private readonly IDataProtectionProvider _dataProtectionProvider;
		private const string Key = "cut-the-night-with-the-light";

		public CryptoHelper(IDataProtectionProvider dataProtectionProvider)
		{
			_dataProtectionProvider = dataProtectionProvider;
		}

		public string Encrypt(string input)
		{
			var protector = _dataProtectionProvider.CreateProtector(Key);
			return protector.Protect(input);
		}

		public string Decrypt(string input)
		{
			var protector = _dataProtectionProvider.CreateProtector(Key);
			return protector.Unprotect(input);
		}
	}
}