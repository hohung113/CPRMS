﻿namespace Rms.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public virtual UserSystem User { get; set; }
        public virtual Role Role { get; set; }
    }
}
