using Application.Common.Interfaces;
using Core.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IHttpContextAccessor _httpContext;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions,
            IHttpContextAccessor httpContextAccessor) : base(dbContextOptions)
        {
            _httpContext = httpContextAccessor;
        }

        public IDbConnection GetConnection()
        {
            var connection = Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserToRole> UserRoles { get; set; }
        public DbSet<RoleToClaim> RoleToCalims { get; set; }
        public DbSet<OtpValidation> OtpValidations { get ; set ; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                /*var userIdentity = _jwtDecodeData.JwtTokenDecodeData(_httpContext);
                var userId = 0;
                if (userIdentity.Count == 0)
                {
                    userId = 1;
                }
                else
                {
                    userId= Convert.ToInt32(userIdentity[1].Value);
                }*/
                /*foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.created_by = userId;
                            entry.Entity.created_datetime = _dateTime.UtcNow;
                            break;

                        case EntityState.Modified:
                            entry.Entity.modified_by = userId;
                            entry.Entity.modified_datetime = _dateTime.UtcNow;
                            break;
                    }
                }*/

                var result = await base.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /* protected override void OnModelCreating(ModelBuilder builder)
         {
             builder.HasDefaultSchema("app");
             builder.Entity<Patient>().Property(p => p.empi).ValueGeneratedNever();
             builder.Entity<UserToRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
             builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
             SeedingData.PopulateSampleData(ref builder);
             base.OnModelCreating(builder);
         }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.LogTo(Console.WriteLine);
    }
}
