using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.FixType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Dto
{
    public class ContentNodeDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }
        public ContentPageType ContentPageType { get; set; }

        public ICollection<ContentPageDto> Pages { get; set; }
    }

    public class BlogNodeDto : ContentNodeDto
    {
        public virtual IList<BlogPageDto> Pages { get; set; }

    }
}
