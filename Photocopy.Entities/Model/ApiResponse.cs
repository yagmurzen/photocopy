using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Model
{
    public class ApiResponse : BaseResponse
    {
        public ApiResponseModel response { get; set; }

        public ApiResponse(bool success, string message,ApiResponseModel response) : base(success, message)
        {
            this.response = response;
        }
        public ApiResponse(ApiResponseModel response) : this(true, "İşlem Başarılı", response)
        { }

        public ApiResponse(string message) : this(false, message, null)
        { }


    }
}
