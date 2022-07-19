using Application.Common.Interfaces;
using Application.UserCQ.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCQ.Query
{
    public class GetUserDetailById :IRequest<UserResponseDto>
    {
        public int Id { get; set; }
    }
    public class GetUserDetailByIdHandler : IRequestHandler<GetUserDetailById, UserResponseDto>
    {
        UserResponseDto userResponseDto;
        private readonly IApplicationDbContext _context;
        public GetUserDetailByIdHandler(IApplicationDbContext context)
        {
            _context = context;
            userResponseDto = new UserResponseDto();    
        }
        public async Task<UserResponseDto> Handle(GetUserDetailById request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FindAsync(request.Id).Result;
            UserResponseDto userResponseDto = new UserResponseDto();
            userResponseDto.userId = request.Id;
            userResponseDto.name = user.FirstName + " " + user.LastName;
            userResponseDto.email = user.Email;
            return userResponseDto;
        }
    }
}
