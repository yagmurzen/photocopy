using AutoMapper;
using Photocopy.Core.Interface.Repository;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Service.Services.WebUI
{
    public class FaqService : IFaqService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FaqService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public FaqDto GetFaqById(int faqId)
        {
            Faq faq = _unitOfWork.Faqs.GetByIdAsync(x => x.Id == faqId && !x.IsDeleted);

            FaqDto outModel = _mapper.Map<FaqDto>(faq);

            return outModel;
        }

        public FaqDto SaveOrUpdate(FaqDto faq)
        {
            Faq inModel = _mapper.Map<Faq>(faq);

            if (faq.Id != null) _unitOfWork.Faqs.Update(inModel);
            else _unitOfWork.Faqs.AddAsync(inModel);
            _unitOfWork.CommitAsync();

            return _mapper.Map<FaqDto>(inModel);

        }

        public IList<FaqDto> GetFaqList()
        {
            var faqs = _unitOfWork.Faqs.GetAllAsync(x => !x.IsDeleted).Result.ToList();
            IList<FaqDto> faqlist = _mapper.Map<IList<FaqDto>>(faqs);
            return faqlist;
        }

        public async void DeleteFaq(int faqId)
        {

            Faq faq = _unitOfWork.Faqs.GetByIdAsync(x => !x.IsDeleted && x.Id == faqId);
            faq.IsDeleted = true;
            await _unitOfWork.Faqs.Update(faq);
            _unitOfWork.CommitAsync();
        }
    }
}
