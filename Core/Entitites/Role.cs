using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entitites
{
    public class Role
    {
        [Key]
        public int role_Id { get; set; }
        public string Name { get; set; }
        public bool active { get; set; }
        public string created_by { get; set; }
        public DateTime create_datetime { get; set; }
    }
}
