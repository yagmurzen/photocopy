using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.FixType;
using Photocopy.Entities.Dto;
using System.Xml.Linq;

namespace Photocopy.CMS.Controllers
{
    public class ContentNodeController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private IContentNodeService _service;
        private readonly IMapper _mapper;

        public ContentNodeController(ILogger<AuthController> logger, IContentNodeService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }


        #region Content Node
        public IActionResult ContentNode(int? contentId)
        {
            ContentNodeDto outModel = new ContentNodeDto() ;
            if (contentId!=null) outModel = _service.GetContentNodeDetail(contentId.Value);

           
            return View("/Views/ContentNode/ContentNode.cshtml", outModel);
        }
        public IActionResult ContentNodeList()
        {
            var model =_service.GetContentNodeAll();

            return View("/Views/ContentNode/ContentNodeList.cshtml", model);

        }

        public IActionResult SaveOrUpdateContentNode(int? Id, string Name)
        {
           ContentNodeDto node= _service.SaveOrUpdateContentNode(new ContentNodeDto { Id = Id, Name = Name,ContentPageType= ContentPageType.Static });

            return ContentNode(node.Id);

        } 
        public IActionResult DeleteContentNode(int contentId)
        {

            _service.DeleteContentNode(contentId);          
            return ContentNodeList();

        }
        #endregion

        #region Content Page

        public IActionResult ContentPage(int contentNodeId, int? contentPageId)
        {
            ContentPageDto outModel = new ContentPageDto { Id=contentPageId,ContentNodeId=contentNodeId};
            if (contentPageId!=null)  outModel = _service.GetContentPageDetail(contentPageId.Value);

            return View("/Views/ContentNode/ContentPage.cshtml", outModel);
        }

        public IActionResult SaveOrUpdateContentPage(ContentPageDto page, IFormFile MainImagePath, IFormFile ThumbImagePath)
        {

            page.MainImagePath = Base64ToImage(MainImagePath);
            page.ThumbImagePath = Base64ToImage(ThumbImagePath);
            ContentPageDto pageModel = _service.SaveOrUpdateContentPage(page);          

            return ContentPage(pageModel.ContentNodeId, pageModel.Id);

        }

        private string Base64ToImage(IFormFile file)
        {
            string str = "";
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                str = Convert.ToBase64String(fileBytes);
                // act on the Base64 data
            }

            return str;
        }

        //public IActionResult DeleteContentPage(int contentPageId,int contentNodeId)
        //{
        //    #region Delete İşlemleri
        //    Db.ContentPages.Remove(new ContentPage { Id = contentPageId });
        //    Db.SaveChanges();

        //    #endregion

        //    return ContentNode(contentNodeId);

        //}
        #endregion

        //#region ContentGallery


        //public IActionResult ContentGallery(string contentGalleryId)
        //{
        //    Galery gallery = Db.Galeries.Where(x => x.Id == Convert.ToInt32(contentGalleryId)).SingleOrDefault() ?? new Galery();
        //    IList<GaleryFile> galleryFiles = Db.GaleryFiles.Where(x => x.GaleryId == gallery.Id).ToList();
        //    gallery.GaleryFiles = galleryFiles;
        //    return View("/Views/ContentNode/ContentGallery.cshtml", gallery);

        //}

        //public IActionResult SaveOrUpdateContentGallery(Galery gallery)
        //{
        //    Galery cgallery = new Galery();
        //    #region Update İşlemleri
        //    if (gallery != null)
        //    {
        //        cgallery = Db.Galeries.Where(x => x.Id == gallery.Id).Single();
        //        cgallery.Name = gallery.Name;
        //        Db.Update(cgallery);

        //    }
        //    else
        //    {
        //        cgallery = gallery;
        //        Db.Add(gallery);

        //    }



        //    Db.SaveChanges();

        //    #endregion

        //    return View("/Views/ContentNode/ContentGallery.cshtml", cgallery);

        //}

        //public IActionResult DeleteContentGallery(int contentNodeId)
        //{
        //    #region Delete İşlemleri
        //    Db.ContentPages.Remove(new ContentPage { Id = contentNodeId });
        //    Db.SaveChanges();

        //    #endregion

        //    return ContentNodeList();

        //}

        //#endregion

        //#region Gallery File      

        //public IActionResult SaveOrUpdateContentGalleryFile(GaleryFile gallery)
        //{
        //    GaleryFile cgallery = new GaleryFile();
        //    #region Update İşlemleri
        //    if (gallery != null)
        //    {
        //        cgallery = Db.GaleryFiles.Where(x => x.Id == gallery.Id).Single();
        //        cgallery.Name = gallery.Name;
        //        cgallery.Description = gallery.Description;

        //        cgallery.Title = gallery.Title;
        //        cgallery.Name = gallery.Name;

        //        Db.Update(cgallery);

        //    }
        //    else
        //    {
        //        cgallery = gallery;
        //        Db.Add(gallery);

        //    }



        //    Db.SaveChanges();

        //    #endregion

        //    return ContentGallery("1");

        //}

        //public IActionResult DeleteContentGalleryFile(int contentNodeId)
        //{
        //    #region Delete İşlemleri
        //    Db.ContentPages.Remove(new ContentPage { Id = contentNodeId });
        //    Db.SaveChanges();

        //    #endregion

        //    return ContentNodeList();

        //}

        //#endregion

    }
}
