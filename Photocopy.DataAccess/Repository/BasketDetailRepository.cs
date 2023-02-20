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
    public class BasketDetailRepository : Repository<BasketDetail>, IBasketDetailRepository
    {
        public BasketDetailRepository(ApplicationContext context) : base(context)
        {
        }

        public IList<BasketDetail> GetAllBasketDetails(Expression<Func<BasketDetail, bool>> predicate)
        {
            return Context.Set<BasketDetail>().Include(x => x.UploadData).AsNoTracking().Where(predicate).ToList();
        }

    }
}
