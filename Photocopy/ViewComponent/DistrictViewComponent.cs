using Azure;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Dto.WebUI;
using Photocopy.Entities.Model;
using Photocopy.Helper;

namespace Photocopy.UI.Component
{
    public class DistrictViewComponent : ViewComponent
    {
        private IContentService _service;
        private readonly IHttpClientExtensions _httpClientExtensions;

        public DistrictViewComponent(IContentService service, IHttpClientExtensions httpClientExtensions)
        {
            _service = service;
            _httpClientExtensions = httpClientExtensions;
        }

        public async Task<IViewComponentResult> InvokeAsync(string cityId)
        {
            if (!String.IsNullOrEmpty(cityId))
            {


                ApiResponse response = _httpClientExtensions.GetDistrict(cityId);
                //{ "cityCode":"03","cityName":"AFYON","code":"85","name":"SİNANPAŞA"}
                //var districts = _service.GetAllDisctirtById(cityId);

                if (response.Success)
                {
                    IList<DistrictDto> districts = JsonHelper.Deserialize<IList<DistrictDto>>(response.response.Repsonse);

                    return View(districts);
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
    }
}
