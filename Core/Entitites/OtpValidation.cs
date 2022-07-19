using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entitites
{
    public class OtpValidation
    {
        [Key]
        public int id { get; set; }
        public int user_id { get; set; }
        public string email { get; set; }
        public string token { get; set; }
        public DateTime tokenTime { get; set; }
        public int tokenExpireTime { get; set; }
        [ForeignKey("user_id")]
        public User User { get; set; }
    }
}
