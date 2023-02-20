using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.Interface.Services;

namespace Photocopy.UI.Component
{
    public class FaqBoxViewComponent : ViewComponent
    {
        private IContentService _service;

        public FaqBoxViewComponent(IContentService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model=_service.GetQuestion();
            return View(model);
        }
    }
}
