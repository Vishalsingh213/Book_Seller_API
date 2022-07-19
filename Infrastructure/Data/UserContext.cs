using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<Core.Entitites.User> user { get ; set ; }
        public DbSet<Core.Entitites.Role> role { get; set; }
        public DbSet<Core.Entitites.RoleToClaim> roleToClaim { get; set; }
    }
}
