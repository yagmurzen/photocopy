using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Domain
{
    public class Menu : EntityBase
    {
        public  string Name { get; set; }
        public  string Code { get; set; }
        public virtual ContentNode ContentNode { get; set; }
        public  int ContentNodeId { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }

    }
    public class MenuItem : EntityBase
    {
        public int Position { get; set; }
        public string Link { get; set; }
        public virtual Menu Menu { get; set; }
        public int MenuId { get; set; }
        public virtual ContentNode ContentNode { get; set; }
        public int ContentNodeId { get; set; }

        public virtual MenuItem ParentItem { get; set; }
        public int MenuItemId { get; set; }
        public ICollection<MenuItem> ParentItems { get; } = new List<MenuItem>();

    }
}