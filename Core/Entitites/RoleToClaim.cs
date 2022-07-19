using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entitites
{
    public class RoleToClaim
    {
        [Key]
        public int Id { get; set; }

        public int role_Id { get; set; }
        public string claim_type { get; set; }
        public bool claim_value { get; set; }
        [ForeignKey("role_Id")]
        public Role Role { get; set; }
    }
}
