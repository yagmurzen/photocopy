using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.Interface.Services;

namespace Photocopy.UI.Component
{
    public class BlogBoxViewComponent : ViewComponent
    {
		private IContentService _service;

		public BlogBoxViewComponent(IContentService service)
		{
			_service = service;
		}

		public async Task<IViewComponentResult> InvokeAsync()
        {
			var blogs = _service.GetBlogs();

            return View(blogs);
        }
    }
}
