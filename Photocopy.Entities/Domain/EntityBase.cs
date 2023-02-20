using System;

namespace Photocopy.Entities.Domain
{
    public class EntityBase
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}