using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;

namespace Photocopy.CMS.Controllers
{
    public class FaqController : Controller
    {
        private readonly ILogger<FaqController> _logger;
        private IFaqService _service;
        private readonly IMapper _mapper;

        public FaqController(ILogger<FaqController> logger, ApplicationContext db, IMapper mapper, IFaqService service)
        {
            _logger = logger;
           _service= service;
            _mapper = mapper;
        }

        public IActionResult Faq(int faqId)
        {
            FaqDto faq = _service.GetFaqById(faqId) ??  new FaqDto();

            return View(faq);

        }
        public IActionResult FaqList()
        {
            IList<FaqDto> faqs = _service.GetFaqList();
            return View(faqs);

        }
        public IActionResult SaveOrUpdateFaq(FaqDto faq)
        {

            _service.SaveOrUpdate(faq);

            return View("/Views/Faq/Faq.cshtml", faq);

        }

        public IActionResult DeleteFaq(int faqId)
        {
            _service.DeleteFaq(faqId);
            return FaqList();

        }
    }
}
