using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entitites
{
    [Table("header")]
    public class Header
    {
        [Key]
        public int id { get; set; }

        [Column("Name", TypeName = "NVARCHAR(100)")]
        public string Name{ get; set; }

        [Column("Description", TypeName = "NVARCHAR(500)")]
        public string Description { get; set; }
        
        [Column("Is_Default")]
        public Boolean IsDefault{ get; set; } = false;

        [Column("Is_Active")]
        public Boolean IsActive { get; set; } = true;

        [Column("Parent_Of")]
        public int ParentOf{ get; set; }

        public int Order{ get; set; }

        [Column("ReportId", TypeName = "NVARCHAR(500)")]
        public string ReportId { get; set; }
    }
}
