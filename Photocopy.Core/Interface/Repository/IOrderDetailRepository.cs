using Photocopy.Entities.Domain;
using System.Linq.Expressions;

namespace Photocopy.Core.Interface.Repository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        IEnumerable<OrderDetail> GetOrderDetails(Expression<Func<OrderDetail, bool>> predicate);
    }
}
