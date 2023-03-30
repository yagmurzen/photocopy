using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.FixType;
using Photocopy.Entities.Dto;
using System.Xml.Linq;

namespace Photocopy.CMS.Controllers
{
    public class SliderController : Controller
    {
        private readonly ILogger<SliderController> _logger;
        private ISliderService _service;
        private readonly IMapper _mapper;

        public SliderController(ILogger<SliderController> logger, ISliderService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("cms/slider")]
        public IActionResult Slider(int sliderId)
        {
            SliderDto slider = _service.GetSliderById(sliderId) ?? new SliderDto(); ;
            return View("/Views/_CMS/Slider/Slider.cshtml", slider);

        }
        [HttpGet]
        [Route("cms/slider-listesi")]
        public IActionResult SliderList()
        {
            IList<SliderDto> sliders = _service.GetSliderList();
            return View("/Views/_CMS/Slider/SliderList.cshtml", sliders);

        }
        [HttpPost]
        [Route("cms/slider")]
        public async Task<IActionResult> SliderAsync(SliderDto slider)
        {

            await _service.SaveOrUpdateAsync(slider);

            return RedirectToAction("slider","cms", new { sliderId = slider.Id });

        }
        [Route("cms/DeleteSlider")]
        public IActionResult DeleteSlider(int sliderId)
        {
            _service.DeleteSlider(sliderId);
            return RedirectToAction("slider-listesi", "cms" );

        }
    }
}
