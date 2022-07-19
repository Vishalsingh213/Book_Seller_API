using Application.Common.Interfaces;
using AutoMapper;
using Application.Mappers;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Constants;
using Microsoft.Extensions.Configuration;

namespace Application.LoginCQ.Querries
{
    public  class GetUsersQuerry : IRequest<List<UserDto>>
    {
        public int id { get; set; }
    }

    public class GetUsersQuerryHandler : IRequestHandler<GetUsersQuerry, List<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private static IConfigurationSection _jwtSettings { get; set; }


        public GetUsersQuerryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuerry request, CancellationToken cancellationToken)
        {

            var result = await GetUserListDetail();
            return result;

        }
        public async Task<List<UserDto>> GetUserListDetail()
        {
            using (var con = _context.GetConnection())
            {
                var UserList = (await con.QueryAsync<UserDto>(DapperConstants.get_users_list,
                           commandType: CommandType.StoredProcedure)).AsList<UserDto>();

                return UserList;
            }
        }
    }
}
