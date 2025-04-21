using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid CreateBy { get; set; }
        public Guid UpdateBy { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; } = new byte[8];
        //public Guid? EncryptionKeyId { get; set; }
        //public EState State { get; set; }
        public bool IsDelete { get; set; }
    }
}
