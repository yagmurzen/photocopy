using Azure.Core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.Interface.Helper;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Dto.WebUI;
using Photocopy.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Photocopy.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private IContentService _contentService;
        private IOrderService _service;
		ICryptoHelper _cryptoHelper;

		public BlogController(ILogger<BlogController> logger, IContentService contentService, IOrderService service, ICryptoHelper cryptoHelper)
		{
			_logger = logger;
			_contentService = contentService;
			_service = service;
			_cryptoHelper = cryptoHelper;
		}

		[Route("blog")]
		public IActionResult Blog()
		{
			var blogs=_contentService.GetBlogs();
			return View(blogs);
		}

		[Route("blog/{slug}")]
		public IActionResult BlogDetail(string slug)
		{
			var blog= _contentService.GetBlogDetail(slug);

			return View(blog);
		}
	}
}