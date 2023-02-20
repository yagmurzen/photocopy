using System;
using System.Collections.Generic;
using DudesCms.Core.Service.Dto.Site;

namespace WebUI.Model
{
    public class TextboxDto
    {
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string Label { get; set; } 
        public string Value { get; set; } 
        public string PlaceHolder { get; set; } 
        public string CssClass { get; set; } 
        public bool IsRequired { get; set; }
        public bool Readonly { get; set; } 
 
        public int MaxLength { get; set; } 
    }
    public class DateboxDto
    {
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string Label { get; set; } 
        public DateTime Value { get; set; } 
        public string PlaceHolder { get; set; } 
        public string CssClass { get; set; } 
        public bool IsRequired { get; set; } 
    }
    public class ImageDto
    {
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string Label { get; set; } 
        public string Value { get; set; }
        public int Width { get; set; }
        public int Height { get; set; } 
        public string PlaceHolder { get; set; } 
        public string CssClass { get; set; } 
        public bool IsRequired { get; set; } 
    }
    public class FileDto
    {
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string Label { get; set; } 
        public string Value { get; set; } 
        public string PlaceHolder { get; set; } 
        public string CssClass { get; set; } 
        public bool IsRequired { get; set; } 
    }
    public class CheckboxDto
    {
        public string Name { get; set; } 
        public string Label { get; set; } 
        public bool IsChecked { get; set; } 
        public string CssClass { get; set; } 
        public bool IsRequired { get; set; } 
    }
    public class SelectDto
    {
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string Label { get; set; }
        public string PlaceHolder { get; set; } 
        public Guid SelectedValue { get; set; } 
        public string CssClass { get; set; } 
        public bool IsRequired { get; set; }
        public IList<SelectItemDto> Items { get; set; } 
    }

    public class Select2Dto
    {
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string Label { get; set; }
        public string PlaceHolder { get; set; }
        public string SelectedValue { get; set; } 
        public string CssClass { get; set; } 
        public bool IsRequired { get; set; }
        public IList<SelectListDto> Items { get; set; } 
    }

    
}