using Photocopy.Entities.Domain;
using System.Linq.Expressions;

namespace Photocopy.Core.Interface.Repository
{
    public interface IBasketDetailRepository : IRepository<BasketDetail>
    {
       IList<BasketDetail> GetAllBasketDetails(Expression<Func<BasketDetail, bool>> predicate);

    }
}
