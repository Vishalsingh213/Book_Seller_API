using System;

namespace Innoid.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime created_datetime { get; set; }
        public int created_by { get; set; }
        public DateTime? modified_datetime { get; set; }
        public int? modified_by { get; set; }
    }
}
