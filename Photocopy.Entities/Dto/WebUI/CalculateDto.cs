using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Dto.WebUI
{


    public class LookupPriceDto
    {

        //public IList<Format> Formats { get; set; }
        //public IList<Combination> Combination { get; set; }
        //public IList<Side> Side { get; set; }
        //public IList<Rotate> Rotate { get; set; }
        //public IList<LookupPriceDto> Bindings { get; set; }
        //public IList<LookupPriceDto> PaperType { get; set; }

        public string Tag { get; set; }
        public string Text { get; set; }
        public float DefaultValue { get; set; }
        public float Colurful { get; set; }
        public float Quality { get; set; }
    }

    public class CalculateDto
    {
        public Format Format { get; set; }

        public Combination Combination { get; set; }
        public Side Side { get; set; }
        public Rotate Rotate { get; set; }

        public int Colourfull { get; set; }
        public int Quality { get; set; }

        public Binding Binding { get; set; }
        public PaperType PaperType { get; set; }

        public int PdfPageCount { get; set; }
        public int Count { get; set; }
        public Guid UploadDataId { get; set; }
        public string FileName { get; set; }
        public decimal PagePrice { get; set; }
        public decimal FilePrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
    
    public enum Format
    {
        A4,
        A5,
        A3
    }

    public enum Combination
    {
        X1,
        X2,
        X4,
        X6
    }

    public enum Side
    {
        TEK,
        CIFT
    }
    public enum Rotate
    {
        DIKEY,
        YATAY
    }
    public enum ColourFull
    {
        SIYAHBEYAZ,
        RENKLI
    }
    public enum Quality
    {
        STANDART,
        YUKSEK
    }
    public enum PaperType
    {
        _80_Gram
    }
    public enum Binding
    {
        Sprial,
        Kenardan_Zımba, 
        Sol_Ustten_Zımba,
        Istemiyorum
    }
}
