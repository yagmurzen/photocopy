using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.Domain;
using Photocopy.Entities.Dto.WebUI;
using System.Collections;
using System.Linq.Expressions;

namespace Photocopy.Core.Interface.Repository
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<Basket> CreateBasket(Basket entity);

        Task<Basket> UpdateBasket(Basket entity);
    }
}
