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

namespace Photocopy.Service.Services
{
    public class SliderService : ISliderService
    {

        private IUnitOfWork  _unitOfWork;
        private readonly IMapper _mapper;

        public SliderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public SliderDto GetSliderById(int sliderId)
        {
            Slider slider = _unitOfWork.Sliders.Find(x => !x.IsDeleted && x.Id==sliderId).SingleOrDefault() ??  new Slider();

            SliderDto outModel = _mapper.Map<SliderDto>(slider);

            return outModel;
        }

        public async Task<SliderDto> SaveOrUpdateAsync(SliderDto slider)
        {
            Slider inModel = _mapper.Map<Slider>(slider);

            if (slider.Id!=null)
                _unitOfWork.Sliders.Update(inModel);
            else _unitOfWork.Sliders.AddAsync(inModel);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<SliderDto>(inModel);

        }

        public IList<SliderDto> GetSliderList()
        {
            var sliders = _unitOfWork.Sliders.GetAllAsync(x =>!x.IsDeleted).Result.ToList() ;
            IList<SliderDto> sliderlist= _mapper.Map<IList<SliderDto>>(sliders);
            return sliderlist;
        }

        public async Task DeleteSlider(int sliderId)
        {
            Slider slider =  _unitOfWork.Sliders.GetByIdAsync(x => !x.IsDeleted && x.Id == sliderId);
            if (slider!=null)
            {
                slider.IsDeleted = true;
                await _unitOfWork.Sliders.Update(slider);
                await _unitOfWork.CommitAsync();
            }
         
        }
    }
}
