using System.Collections.Generic;
using System;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.FixType;

namespace Photocopy.Entities.Domain
{
    public class ContentNode : EntityBase
    {

        public string Name { get; set; }
        public virtual Menu Menu { get; set; }
        public ContentPageType ContentPageType { get; set; }
        public  ICollection<ContentPage> Pages { get; set; }
        public ICollection<Galery> GaleryList { get; set; }

    }


    //public class StaticNode : ContentNode
    //{
    //    public virtual IList<StaticPage> Pages { get; set; }
    //}

    public class BlogNode : ContentNode
    {
        public virtual IList<BlogPage> Pages { get; set; }

    }
    //public class SpecialNode : ContentNode
    //{
    //    public virtual ModuleStatus ModuleStatus { get; set; }
    //}

    //public class HomeNode : SpecialNode
    //{
    //    public virtual IList<HomePage> Pages { get; set; }
    //}

}