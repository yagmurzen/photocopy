using Microsoft.AspNetCore.DataProtection;
using Photocopy.Core.Interface.Helper;
using Photocopy.Entities.Dto.WebUI;
using System.Net;
using System.Text.Json;

namespace Photocopy.Helper
{   

    public static class JsonHelper 
    {
        public static object Deserialize(string str)
        {
            return JsonSerializer.Deserialize<object>(str);
        }

        public static T Deserialize<T>(dynamic str)
        {
            return JsonSerializer.Deserialize<T>(str);
        }

        public static string Serialize(dynamic str)
        {
            return JsonSerializer.Serialize(str);
        }
    }
}