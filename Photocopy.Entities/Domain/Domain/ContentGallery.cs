using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Domain
{
    public class ContentGallery : EntityBase
    {
        public virtual ContentNode ContentNode { get; set; }
        public virtual string Name { get; set; }
        public virtual int Position { get; set; }
    }
    public class ContentSlider : EntityBase
    {
        public virtual ContentNode ContentNode { get; set; }
        public virtual string Name { get; set; }
        public virtual int Position { get; set; }
    }
}