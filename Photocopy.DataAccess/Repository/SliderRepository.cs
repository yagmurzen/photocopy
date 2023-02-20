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
    public class CustomerAddressRepository : Repository<CustomerAddress>, ICustomerAddressRepository
    {
        public CustomerAddressRepository(ApplicationContext context) : base(context)
        {
        }

        public CustomerAddress GetCustomerAddress(Expression<Func<CustomerAddress, bool>> predicate)
        {
            return Context.Set<CustomerAddress>().Include(x => x.City).AsNoTracking().Where(predicate).FirstOrDefault();
        }
     
    }
}
