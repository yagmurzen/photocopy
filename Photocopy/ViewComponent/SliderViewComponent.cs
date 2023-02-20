using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.Interface.Services;

namespace Photocopy.UI.Component
{
    public class SliderViewComponent : ViewComponent
    {
        private IContentService _service;

        public SliderViewComponent(IContentService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = _service.GetSliderlist();

            return View(sliders);
        }
    }
}
