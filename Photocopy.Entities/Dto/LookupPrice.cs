namespace Photocopy.Entities.Dto
{
    public class LookupPriceListDto
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string TagValue { get; set; }
        public string Text { get; set; }
        public double DefaultValue { get; set; }
        public int IsColourful { get; set; }
        public int IsQuality { get; set; }
    }


    public class SetLookupPriceCmsDto
    {
        public int Id { get; set; }
        public double DefaultValue { get; set; }
    }
}
