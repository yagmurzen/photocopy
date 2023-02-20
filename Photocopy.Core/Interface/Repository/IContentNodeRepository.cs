using Photocopy.Entities.Domain;
using System.Linq.Expressions;

namespace Photocopy.Core.Interface.Repository
{
    public interface IContentNodeRepository : IRepository<ContentNode>
    {
        IEnumerable<ContentNode> GetAllContentNodeAsync(Expression<Func<ContentNode, bool>> predicate);
        ContentNode GetContentNodeAsync(Expression<Func<ContentNode, bool>> predicate);
        void RemoveContentNode(Expression<Func<ContentNode, bool>> predicate);

        IEnumerable<ContentPage> GetContentPageByIdAsync(Expression<Func<ContentPage, bool>> predicate);
        Task<ContentPage> AddContentPageAsync(ContentPage entity);
        void RemoveContentPage(ContentPage entity);

        IEnumerable<Slider> GetSliderAsync(Expression<Func<Slider, bool>> predicate);


    }
}
