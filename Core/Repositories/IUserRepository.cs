﻿using Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUserRepository : IRepository<Core.Entitites.User>
    {
        //custom operations here
        Task<IEnumerable<Core.Entitites.User>> GetUserByLastName(string lastname);
    }
}
