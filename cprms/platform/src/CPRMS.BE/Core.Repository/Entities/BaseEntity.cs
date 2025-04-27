using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public Guid LastModifiedBy { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; } = new byte[8];
        public bool IsDeleted { get; set; }
        //public Guid? EncryptionKeyId { get; set; }
        //public EState State { get; set; }
    }
}