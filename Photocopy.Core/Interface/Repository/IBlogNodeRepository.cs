using Photocopy.Entities.Domain;
using System.Linq.Expressions;

namespace Photocopy.Core.Interface.Repository
{
    public interface IBlogNodeRepository : IRepository<BlogNode>
    {
        Task<IEnumerable<BlogPage>> GetAllPageAsync(Expression<Func<BlogPage, bool>> predicate);
        IEnumerable<BlogPage> GetBlogPageByIdAsync(Expression<Func<BlogPage, bool>> predicate);
        void RemoveBlogNode(Expression<Func<BlogNode, bool>> predicate);
        void RemoveBlogPage(BlogPage entity);
        Task<BlogPage> AddBlogPageAsync(BlogPage entity);
        Task<BlogPage> UpdateBlogPage(BlogPage entity);


	}
}
