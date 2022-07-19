using Core.Entitites;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        IDbConnection GetConnection();
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserToRole> UserRoles { get; set; }
        DbSet<RoleToClaim> RoleToCalims { get; set; }

        DbSet<OtpValidation> OtpValidations { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
        //8329901689
  
    }
}