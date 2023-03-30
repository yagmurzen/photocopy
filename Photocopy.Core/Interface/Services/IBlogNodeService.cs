using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Core.Interface.Services
{
    public interface IBlogNodeService
    {
        BlogNodeDto GetBlogNodeDetail(int BlogNodeId);
        IEnumerable<BlogNodeDto> GetBlogNodeAll();
        BlogPageDto GetBlogPageDetail(int BlogPageId);

        Task DeleteBlogNode(int BlogNodeId);
        Task DeleteBlogPage(int BlogPageId);

        Task<BlogNodeDto> SaveOrUpdateBlogNodeAsync(BlogNodeDto BlogNode);
        Task<BlogPageDto> SaveOrUpdateBlogPageAsync(BlogPageDto BlogPage);
    }
}
