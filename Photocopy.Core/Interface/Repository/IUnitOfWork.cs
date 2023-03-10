using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Core.Interface.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogNodeRepository Blogs { get; }

        IContentNodeRepository Contents { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        ISliderRepository Sliders { get; }
        IFaqRepository Faqs { get; }

        IUserRepository Users { get; }
        ICustomerAddressRepository CustomerAddresses { get; }
        IOrderDetailRepository OrderDetails { get; }

        ICityRepository Cities { get; }
        IDistrictRepository Districts { get; }
        ICargoFirmRepository CargoFirms { get; }
        IBasketRepository Baskets { get; }
        IBasketDetailRepository BasketDetails { get; }
        IContactRepository Contacts { get; }

        Task<int> CommitAsync();
    }
}
