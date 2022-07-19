
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<Core.Entitites.User>, IUserRepository
    {
        public UserRepository(UserContext userContext) : base(userContext) { }
        public async Task<IEnumerable<Core.Entitites.User>> GetUserByLastName(string lastname)
        {
            return await _userContext.user.Where(m => m.LastName == lastname).ToListAsync();
        }

        //public async Task<IEnumerable<UserResponse>> GetAll()
        //{
        //    return (IEnumerable<UserResponse>)await _userContext.user.ToListAsync();
        //}
    }
}
