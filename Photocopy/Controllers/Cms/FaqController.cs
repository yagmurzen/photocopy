using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using System.Xml.Linq;

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
        [Route("cms/sikca-sorulan-sorular-detay")]
        public IActionResult Faq(int faqId)
        {
            FaqDto faq = _service.GetFaqById(faqId) ??  new FaqDto();

            return View("/Views/_CMS/Faq/Faq.cshtml", faq);

        }
        [Route("cms/sikca-sorulan-sorular")]
        public IActionResult FaqList()
        {
            IList<FaqDto> faqs = _service.GetFaqList();
            return View("/Views/_CMS/Faq/FaqList.cshtml", faqs);

        }
        [Route("cms/SaveOrUpdateFaq")]        
        public async Task<IActionResult> SaveOrUpdateFaqAsync(FaqDto faq)
        {

            FaqDto model=await _service.SaveOrUpdateAsync(faq);

            return RedirectToAction("sikca-sorulan-sorular-detay", "cms", new { faqId = model.Id } );

        }
        [Route("cms/DeleteFaq")]
        public async Task<IActionResult> DeleteFaqAsync(int faqId)
        {
           await _service.DeleteFaq(faqId);
            return RedirectToAction("sikca-sorulan-sorular","cms");

        }
    }
}
