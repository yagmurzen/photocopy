using System;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.FixType;

namespace Photocopy.Entities.Domain
{
    public class ContentPage: EntityBase
    {
        public  string MainImagePath { get; set; }
        public  string ThumbImagePath { get; set; }
        public  string HtmlContent { get; set; }
        public  string Slug { get; set; }
        public  bool IsActive { get; set; }
        public  string PageTitle { get; set; }
        public  string MetaDescription { get; set; }
        public  string MetaKeyword { get; set; }
        public  string Subtitle { get; set; }
        public  string Description { get; set; }
        public  string CustomCss { get; set; }
        public  string CustomJs { get; set; }
        public virtual ContentNode ContentNode { get; set; }
        public  int ContentNodeId { get; set; }
        public ContentPageType ContentPageType { get; set; }




    }

    public class BlogPage : ContentPage
    {

    }
    //public class StaticPage : ContentPage
    //{

    //}

    //public  class SpecialPage : ContentPage
    //{
    //    public virtual ModuleStatus Status { get; set; }
    //}

    //public class HomePage : SpecialPage
    //{

    //}

    //public class BlogListPage : SpecialPage
    //{

    //}

}