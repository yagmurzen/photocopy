using Photocopy.Core.Interface.Repository;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using Photocopy.Entities.Dto.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Core.Interface.Services
{
    public interface IContentService    {
        
        IList<SliderDto> GetSliderlist();
        IList<FaqDto> GetQuestion();
        IList<BlogListDto> GetBlogs();
        BlogListDetailDto GetBlogDetail(string slug);
        IList<CityDto> GetAllCity();
        IList<DistrictDto> GetAllDisctirtById(string cityId);

    }
}
