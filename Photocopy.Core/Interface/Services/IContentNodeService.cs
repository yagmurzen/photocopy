using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Core.Interface.Services
{
    public interface IContentNodeService
    {
        ContentNodeDto GetContentNodeDetail(int contentNodeId);
        IEnumerable<ContentNodeDto> GetContentNodeAll();
        ContentPageDto GetContentPageDetail(int contentPageId);

        void DeleteContentNode(int contentNodeId);
        void DeleteContentPage(int contentPageId);

        ContentNodeDto SaveOrUpdateContentNode(ContentNodeDto contentNode);
        ContentPageDto SaveOrUpdateContentPage(ContentPageDto contentPage);
        IList<SliderDto> GetSliderlist();
    }
}
