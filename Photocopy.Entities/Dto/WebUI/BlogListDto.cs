using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Dto.WebUI
{
	public class BlogListDto
	{
		public virtual int Id { get; set; }
		public virtual string PageTitle { get; set; }
        public virtual string Description { get; set; }
		public virtual string ThumbImagePath { get; set; }
        public virtual string Slug { get; set; }


    }
}
