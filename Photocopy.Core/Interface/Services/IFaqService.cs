using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Core.Interface.Services
{
    public interface IFaqService
    {
        FaqDto GetFaqById(int faqId);

        FaqDto SaveOrUpdate(FaqDto faq);
        IList<FaqDto> GetFaqList();
        void DeleteFaq(int faqId);


    }
}
