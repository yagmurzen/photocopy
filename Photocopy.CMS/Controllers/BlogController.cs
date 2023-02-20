using Microsoft.AspNetCore.Mvc;
using Photocopy.Entities.Domain.FixType;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using AutoMapper;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Xml.Linq;

namespace Photocopy.CMS.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private IBlogNodeService _service;
        private readonly IMapper _mapper;

        public BlogController(ILogger<BlogController> logger, IBlogNodeService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }


        #region Blog Node
        [HttpGet]
        public IActionResult BlogNodeList()
        {
            var model = _service.GetBlogNodeAll();

            return View("/Views/Blog/BlogNodeList.cshtml", model);

        }

        [HttpGet]
        public IActionResult BlogNode(int? blogId)
        {

            BlogNodeDto outModel = _service.GetBlogNodeDetail(blogId.Value);

            return View("/Views/Blog/BlogNode.cshtml", outModel);
        }	
		[HttpPost]
		public IActionResult BlogNode(int? Id, string Name)
        {
            BlogNodeDto node = _service.SaveOrUpdateBlogNode(new BlogNodeDto { Id = Id, Name = Name, ContentPageType = ContentPageType.Blog });
            return RedirectToAction("BlogNode", new { blogId = node.Id });
        }

		[HttpPost]
		public IActionResult DeleteBlogNode(int blogId)
        {
            _service.DeleteBlogNode(blogId);

            return BlogNodeList();

        }
		#endregion

		#region Blog Page

		[HttpGet]
		public IActionResult BlogPage(int blogId, int? blogPageId)
        {

            BlogPageDto outModel = new BlogPageDto { Id = blogPageId, ContentNodeId = blogId };
            if (blogPageId != null) outModel = _service.GetBlogPageDetail(blogPageId.Value);
            return View("/Views/Blog/BlogPage.cshtml", outModel);
        }
        [HttpPost]
        public async Task<IActionResult> BlogPageAsync(BlogPageDto page, IFormFile MainImagePath, IFormFile ThumbImagePath)
        {

            page.MainImagePath = Base64ToImage(MainImagePath);
            page.ThumbImagePath = Base64ToImage(ThumbImagePath);
            BlogPageDto pageModel = await _service.SaveOrUpdateBlogPageAsync(page);

            return RedirectToAction("BlogPage", new { blogId = pageModel.ContentNodeId, blogPageId = pageModel.Id });


        }
		[HttpPost]
		public IActionResult DeleteBlogPage(int blogPageId, int blogId)
        {
           _service.DeleteBlogPage(blogPageId);


            return BlogNode(blogId);

        }
        #endregion

        private string Base64ToImage(IFormFile file)
        {
            string str = "";

            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    str = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }


            return str;
        }

    }
}
