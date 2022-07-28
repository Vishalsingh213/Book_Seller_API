using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entitites
{
    [Table("Quicksight_Dashboard")]
    public class QuicksightDashboardKey
    {
        [Key]
        public int Id { get; set; }

        [Column("is_active")]
        public Boolean IsActive { get; set; } = true;
        
        public string Key { get; set; }

        [Column("dashboard_name")]
        public string DashboardName { get; set; }

        [Column("dashboard_id")]
        public  string DashboardId { get; set; }

        [Column("dashboard_description")]
        public  string DashboardDescription { get; set; }

        [Column("access_key")]
        public  string AccessKey { get; set; }

        [Column("secret_access_key")]
        public  string SecretAcessKey { get; set; }

        [Column("aws_account_id")]
        public  string AwsAccountId { get; set; }

    }
}
