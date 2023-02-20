using Microsoft.EntityFrameworkCore;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Repository;
using Photocopy.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Photocopy.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<OrderDetail> GetOrderDetails(Expression<Func<OrderDetail, bool>> predicate)
        {
            return Context.Set<OrderDetail>().Include(x=>x.UploadData).AsNoTracking().Where(predicate).ToList();
        }
    }
}
