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
    public class BaseController : Controller
    {
        private readonly ILogger<BaseController> _logger;
        private IOrderService _service;
        ICryptoHelper _cryptoHelper;
        public BaseController(ILogger<BaseController> logger, IOrderService service, ICryptoHelper cryptoHelper)
        {
            _logger = logger;
            _service = service;
            _cryptoHelper = cryptoHelper;
        }
    }
}