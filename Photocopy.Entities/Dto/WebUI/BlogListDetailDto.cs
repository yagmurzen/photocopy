using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Dto.WebUI
{
	public class BlogListDetailDto
	{
		public int? Id { get; set; }

		public string Header { get; set; }
		public string MainImagePath { get; set; }
		public string ThumbImagePath { get; set; }
		public string HtmlContent { get; set; }
		public string Slug { get; set; }
		public string PageTitle { get; set; }
		public string MetaDescription { get; set; }
		public string MetaKeyword { get; set; }
		public string Subtitle { get; set; }
		public string Description { get; set; }
		public string CustomCss { get; set; }
		public string CustomJs { get; set; }
	}
}
