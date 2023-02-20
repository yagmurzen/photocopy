using Microsoft.EntityFrameworkCore;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Repository;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.Domain;
using Photocopy.Entities.Dto.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Photocopy.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext context) : base(context)
        {
        }

        public  IEnumerable<Order> GetAllOrder(Expression<Func<Order, bool>> predicate)
        {
            return  Context.Set<Order>().Include(x => x.OrderState).Include(x => x.CargoCompany).AsNoTracking().Where(predicate).ToList();
        }
        public async Task<Customer> CreateCustomer(Customer entity)
        {
			await Context.Set<Customer>().AddAsync(entity);
            return entity;
		}

        public async Task<Customer> UpdateCustomer(Customer entity)
        {
            Context.Update(entity);

            return entity;
        }


        public async Task<CustomerAddress> CreateCustomerAddress(CustomerAddress entity)
        {
			await Context.Set<CustomerAddress>().AddAsync(entity);
			return entity;
		}

        public async Task<Order> CreateOrder(Order entity)
        {
			await Context.Set<Order>().AddAsync(entity);
			return entity;
		}

        public async Task CreateOrderDetail(IList<OrderDetail> entity)
        {
            await  Context.Set<OrderDetail>().AddRangeAsync(entity);
            
		}

        public async Task<IEnumerable<LookupPrice>> GetAllLookupPrice()
        {
            return await Context.Set<LookupPrice>().ToListAsync();
        }

        public async Task<UploadData> UploadData(UploadData entity)
        {

			await Context.Set<UploadData>().AddAsync(entity);
			return entity;
        }
        public UploadData GetUploadData(Guid id)
        {
            return Context.Set<UploadData>().Where(x => x.Id == id).FirstOrDefault() ?? new UploadData();

        }

        public OrderState GetOrderState(Func<OrderState, bool> predicate)
        {
            return Context.Set<OrderState>().Where(predicate).FirstOrDefault() ??  new OrderState();
        }

        public async Task<IEnumerable<OrderState>> GetAllOrderState(Expression<Func<OrderState, bool>> predicate)
        {
            return await Context.Set<OrderState>().Where(predicate).ToListAsync();
        }

        public Order GetOrderStatus(Expression<Func<Order, bool>> predicate)
        {
            return Context.Set<Order>().Include(x => x.Customer).Include(x => x.OrderState).AsNoTracking().Where(predicate).SingleOrDefault();
        }

        public LookupPrice GetLookupPriceByType(Func<LookupPrice, bool> predicate)
        {
            return Context.Set<LookupPrice>().Where(predicate).Take(1).Single();
        }

        public LookupPrice GetLookupPrice(Expression<Func<LookupPrice, bool>> predicate)
        {
            return Context.Set<LookupPrice>().Where(predicate).Take(1).Single();
        }

        public async Task<LookupPrice> UpdateLookupPrice(LookupPrice entity)
        {
            Context.Update(entity);

            return entity;
        }
    }
}
