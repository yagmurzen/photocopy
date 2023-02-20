using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Dto.WebUI;
using Photocopy.Entities.Model;
using Photocopy.Helper;

namespace Photocopy.UI.Component
{
    public class CityViewComponent : ViewComponent
    {
        private IContentService _service;
        private readonly IHttpClientExtensions _httpClientExtensions;


        public CityViewComponent(IContentService service, IHttpClientExtensions httpClientExtensions)
        {
            _service = service;
            _httpClientExtensions = httpClientExtensions;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ApiResponse response=_httpClientExtensions.GetCities(); //{"code":"01","name":"ADANA  11"}
            //var cities = _service.GetAllCity();

            if (response.Success)
            {
                IList<CityDto> cities = JsonHelper.Deserialize<IList<CityDto>>(response.response.Repsonse);

                return View(cities);
            }
            else
            {
                return View();
            }
            
        }
    }
}
