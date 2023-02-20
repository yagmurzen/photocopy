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
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        public BasketRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Basket> CreateBasket(Basket entity)
        {
            await Context.Set<Basket>().AddAsync(entity);
            return entity;
        }

        public async Task<Basket> UpdateBasket(Basket entity)
        {
            Context.Update(entity);

            return entity;
        }

    }
}
