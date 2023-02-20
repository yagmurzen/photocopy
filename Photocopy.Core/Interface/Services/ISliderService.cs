using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Core.Interface.Services
{
    public interface ISliderService
    {
        SliderDto GetSliderById(int sliderId);

        Task<SliderDto> SaveOrUpdateAsync(SliderDto slider);
        IList<SliderDto> GetSliderList();
        void DeleteSlider(int sliderId);

    }
}
