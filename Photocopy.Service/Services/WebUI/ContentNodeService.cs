using AutoMapper;
using Photocopy.Core.Interface.Repository;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.FixType;
using Photocopy.Entities.Dto;
using Photocopy.Entities.Dto.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Service.Services
{
    public class ContentService : IContentService
    {

		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public ContentService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}



		#region Content Page -- CMS

		public IList<SliderDto> GetSliderlist()
        {
            IList<Slider> sliders = _unitOfWork.Sliders.GetAll(x=> !x.IsDeleted).ToList();

            IList<SliderDto> outModel = _mapper.Map<IList<SliderDto>>(sliders);

            return outModel;
        }

		public IList<FaqDto> GetQuestion()
		{
			var questton = _unitOfWork.Faqs.GetAll(x =>!x.IsDeleted).ToList();
			IList<FaqDto> outModel = _mapper.Map<IList<FaqDto>>(questton);

			return outModel;
		}

		public IList<BlogListDto> GetBlogs()
		{
			var blogs = _unitOfWork.Blogs.GetAllPageAsync(x =>!x.IsDeleted && x.IsActive).Result.OrderByDescending(x=>x.CreatedAt).Take(3).ToList();
			IList<BlogListDto> outModel = _mapper.Map<IList<BlogListDto>>(blogs);

			return outModel;
		}

		public BlogListDetailDto GetBlogDetail(string slug)
		{
			var detail = _unitOfWork.Blogs.GetBlogPageByIdAsync(x => x.IsActive && !x.IsDeleted && x.Slug==slug).ToList();
			BlogListDetailDto outModel = _mapper.Map<BlogListDetailDto>(detail[0]);

			return outModel;
		}



		#endregion


		#region CBS
		public IList<CityDto> GetAllCity()
		{
			var list = _unitOfWork.Cities.GetAll(x => !x.IsDeleted).ToList();
			IList<CityDto> outModel = _mapper.Map<IList<CityDto>>(list);

			return outModel;
		}

		public IList<DistrictDto> GetAllDisctirtById(string cityId)
		{
			var list = _unitOfWork.Districts.Find(x => !x.IsDeleted && x.CityId == cityId).ToList();
			IList<DistrictDto> outModel = _mapper.Map<IList<DistrictDto>>(list);

			return outModel;
		} 
		#endregion
	}
}
