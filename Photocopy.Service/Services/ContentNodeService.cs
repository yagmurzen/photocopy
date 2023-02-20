using AutoMapper;
using Photocopy.Core.Interface.Repository;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.FixType;
using Photocopy.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Service.Services
{
    public class ContentNodeService : IContentNodeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContentNodeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        #region ContentNode
        public IEnumerable<ContentNodeDto> GetContentNodeAll()
        {
            IEnumerable<ContentNode> node = (IEnumerable<ContentNode>)_unitOfWork.Contents.GetAllContentNodeAsync(x => x.ContentPageType == ContentPageType.Static && !x.IsDeleted).ToList();
            return _mapper.Map<IEnumerable<ContentNodeDto>>(node);
        }

        public ContentNodeDto GetContentNodeDetail(int contentNodeId)
        {
            ContentNode node = _unitOfWork.Contents.GetContentNodeAsync(x => x.ContentPageType == ContentPageType.Static && x.Id == contentNodeId && !x.IsDeleted);

            ContentNodeDto outModel = new ContentNodeDto();

            if (node != null)
            {
                IList<ContentPage> pages = _unitOfWork.Contents.GetContentPageByIdAsync(x => x.ContentNodeId == node.Id && !x.IsDeleted ).ToList();
                //IList<Galery> galery = Db.Galeries.Where(x => x.ContentNodeId == node.Id).ToList();

                outModel = new ContentNodeDto
                {
                    Id = node.Id,
                    Name = node.Name,
                    Pages = _mapper.Map<IList<ContentPageDto>>(pages)
                };

            }

            return outModel;

        }

        public ContentNodeDto SaveOrUpdateContentNode(ContentNodeDto contentNode)
        {
            ContentNode nodeModel = _mapper.Map<ContentNode>(contentNode);
            _unitOfWork.Contents.AddAsync(nodeModel);

            return _mapper.Map<ContentNodeDto>(nodeModel);
        }

        public void DeleteContentNode(int contentNodeId)
        {
            _unitOfWork.Contents.Remove(new ContentNode { Id = contentNodeId });
        }

        #endregion


        #region ContenPage
        public ContentPageDto GetContentPageDetail(int contentPageId)
        {
            ContentPage page = _unitOfWork.Contents.GetContentPageByIdAsync(x => x.Id == contentPageId && !x.IsDeleted).Single();

            ContentPageDto outModel = _mapper.Map<ContentPageDto>(page);



            return outModel;
        }

        public ContentPageDto SaveOrUpdateContentPage(ContentPageDto contentPage)
        {
            ContentPage pageModel = _mapper.Map<ContentPage>(contentPage);
            _unitOfWork.Contents.AddContentPageAsync(pageModel);

            return _mapper.Map<ContentPageDto>(pageModel);
        }

        public void DeleteContentPage(int contentPageId)
        {
            _unitOfWork.Contents.RemoveContentPage(new ContentPage { Id = contentPageId });
        }
        #endregion

        #region Slider

        public IList<SliderDto> GetSliderlist()
        {
            IList<Slider> sliders = _unitOfWork.Contents.GetSliderAsync(x=>x.IsActive && !x.IsDeleted).ToList();

            IList<SliderDto> outModel = _mapper.Map<IList<SliderDto>>(sliders);

            return outModel;
        }

        #endregion
    }
}
