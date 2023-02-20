using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Photocopy.Core.Interface.Helper;
using Photocopy.Core.Interface.Repository;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto.WebUI;
using Photocopy.Helper;

namespace Photocopy.UI.Component
{
    public class OrderBoxViewComponent : ViewComponent
    {
        private IContentService _service;

        public OrderBoxViewComponent(IContentService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }

    public class OrderBoxItemViewComponent : ViewComponent
    {
        ICookieHelper _cookieHelper;
        private IOrderService _service;
        private const string orderCookieName = "bskt";
        private IUnitOfWork _unitOfWork;

        public OrderBoxItemViewComponent( ICookieHelper cookieHelper, IOrderService service, IUnitOfWork unitOfWork)
        {
            _cookieHelper = cookieHelper;
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(CalculateDto inModel)        
        {
            // Cookie Kontrol ? 
            var cookie = _cookieHelper.GetCookie(orderCookieName);

            #region DB - Sepet - kontrol
            Basket? basket = null;
            Int32? basketId = null;
            if (!String.IsNullOrEmpty(cookie))
            {
                basketId = Convert.ToInt32(cookie);
                basket = _unitOfWork.Baskets.GetByIdAsync(x => x.IsDeleted == false && x.TransactionDate > DateTime.Now.AddDays(-1) && x.Id == basketId);
                if (basket==null)
                    basket = await _unitOfWork.Baskets.CreateBasket(new Basket { TransactionDate = DateTime.Now });
                else
                {
                    basket.TransactionDate = DateTime.Now;
                    basket = await _unitOfWork.Baskets.UpdateBasket(basket);
                }

                await _unitOfWork.CommitAsync();

            }
            else if(String.IsNullOrEmpty(cookie) && inModel==null)
            {
                return View();
            }

            IList<BasketDetail> basketDetails=_unitOfWork.BasketDetails.GetAllBasketDetails(x =>! x.IsDeleted && x.BasketId == basketId ).ToList();
            #endregion

            IList<CalculateDto> list = new List<CalculateDto>(); // JsonHelper.Deserialize<IList<CalculateDto>>(cookie);

            foreach (var item in basketDetails)
            {
                list.Add(new CalculateDto
                {
                    Binding = item.Binding,
                    Colourfull = item.Colourfull,
                    Combination = item.Combination,
                    Count = item.Count,
                    FileName = item.UploadData.FileName,
                    FilePrice = item.FilePrice,
                    Format = item.Format,
                    PagePrice = item.PagePrice,
                    PaperType = item.PaperType,
                    PdfPageCount = item.PdfPageCount,
                    Quality = item.Quality,
                    Rotate = item.Rotate,
                    Side = item.Side,
                    UnitPrice = item.UnitPrice,
                    UploadDataId=item.UploadDataId
                });

            }

            if (inModel != null)
            {
                if (basket==null)
                {
                    basket = await _unitOfWork.Baskets.CreateBasket(new Basket { TransactionDate = DateTime.Now });
                    await _unitOfWork.CommitAsync();
                }

                //eklenen siparişin tutarı bilgileri hesaplanarak bir dönüş değerine atama yapılır
                list.Add(_service.Calculate(inModel));
                await _unitOfWork.BasketDetails.AddAsync(new BasketDetail
                {
                    Binding = inModel.Binding,
                    Colourfull = inModel.Colourfull,
                    Combination = inModel.Combination,
                    Count = inModel.Count,
                    UploadDataId = inModel.UploadDataId,
                    FilePrice = inModel.FilePrice,
                    Format = inModel.Format,
                    PagePrice = inModel.PagePrice,
                    PaperType = inModel.PaperType,
                    PdfPageCount = inModel.PdfPageCount,
                    Quality = inModel.Quality,
                    Rotate = inModel.Rotate,
                    Side = inModel.Side,
                    UnitPrice = inModel.UnitPrice,
                    BasketId = basket.Id
                });
                await _unitOfWork.CommitAsync();
            }
            _cookieHelper.SetCookie(orderCookieName, basket.Id);

            return View(list);
        }
       
    }
	public class OrderDetailViewComponent : OrderBoxItemViewComponent
	{
		public OrderDetailViewComponent(ICookieHelper cookieHelper, IOrderService service, IUnitOfWork unitOfWork) : base( cookieHelper, service, unitOfWork)
		{
		}
	}
}
