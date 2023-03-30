using Microsoft.AspNetCore.Mvc;
using Photocopy.Entities.Domain.FixType;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using AutoMapper;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Photocopy.CMS.Controllers
{
    [Route("cms")]
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
        [Route("blog-listesi")]
        public IActionResult BlogNodeList()
        {
            var model = _service.GetBlogNodeAll();

            return View("/Views/_CMS/Blog/BlogNodeList.cshtml", model);


        }

        [HttpGet]
        [Route("blog-detay")]
        public IActionResult BlogNode(int blogId)
        {

            BlogNodeDto outModel = _service.GetBlogNodeDetail(blogId);

            return View("/Views/_CMS/Blog/BlogNode.cshtml", outModel);
        }	
		[HttpPost]
        [Route("blog-detay")]
        public async Task<IActionResult> BlogNodeAsync(int Id, string Name)
        {
            BlogNodeDto node =await  _service.SaveOrUpdateBlogNodeAsync(new BlogNodeDto { Id = Id, Name = Name, ContentPageType = ContentPageType.Blog });
            return RedirectToAction("blog-detay","cms", new { blogId = node.Id });
        }

		[HttpPost]
        [Route("DeleteBlogNode")]
        public IActionResult DeleteBlogNode(int blogId)
        {
            _service.DeleteBlogNode(blogId);
            return RedirectToAction("blog-listesi", "cms");
        }
		#endregion

		#region Blog Page

		[HttpGet]
        [Route("blog-sayfasi")]
        public IActionResult BlogPage(int blogId, int? blogPageId)
        {

            BlogPageDto outModel = new BlogPageDto { Id = blogPageId, ContentNodeId = blogId };
            if (blogPageId != null) outModel = _service.GetBlogPageDetail(blogPageId.Value);
            return View("/Views/_CMS/Blog/BlogPage.cshtml", outModel);
        }
        [HttpPost]
        [Route("blog-sayfasi")]
        public async Task<IActionResult> BlogPageAsync(BlogPageDto page, IFormFile MainImagePath, IFormFile ThumbImagePath)
        {

            page.MainImagePath = Base64ToImage(MainImagePath);
            page.ThumbImagePath = Base64ToImage(ThumbImagePath);
            BlogPageDto pageModel = await _service.SaveOrUpdateBlogPageAsync(page);

            return RedirectToAction("blog-sayfasi","cms", new { blogId = pageModel.ContentNodeId, blogPageId = pageModel.Id });


        }
		[HttpPost]
        [Route("DeleteBlogPage")]
        public IActionResult DeleteBlogPage(int blogPageId, int blogId)
        {
            _service.DeleteBlogPage(blogPageId);
            return RedirectToAction("blog-detay","cms", new { blogId = blogId });

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
