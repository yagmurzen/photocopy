using System;

namespace DudesCms.Core.Service.Dto.Site
{
    public class SelectItemDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public bool Selected { get; set; }
    }

    public class SelectListDto
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}