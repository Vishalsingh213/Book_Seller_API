using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entitites
{
    public class UserToRole
    {
        [Key]
        public int id { get; set; }
        public int user_id { get; set; }
        public int role_id { get; set; }
        [ForeignKey("user_id")]
        public User User { get; set; }
        [ForeignKey("role_id")]
        public Role Role { get; set; }
    }
}
