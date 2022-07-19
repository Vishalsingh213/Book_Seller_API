using Application.Common.Constants;
using Application.Common.Interfaces;
using Application.LoginCQ.ViewModel;
using AutoMapper;
using Core.Entitites;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LoginCQ.Querries
{
    public class Login : IRequest<LoginResponseDto>
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class Loginhandler : IRequestHandler<Login, LoginResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        LoginResponseDto responseDto;

        public Loginhandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            responseDto = new LoginResponseDto();
        }
        public async Task<LoginResponseDto> Handle(Login request, CancellationToken cancellationToken)
        {
            User user = _context.Users.Where(x => x.Email.Equals(request.userName)).FirstOrDefault();   //get user 

            if (user != null && user.password.Equals(request.password))
            { 
                responseDto.userName = user.Email;
                responseDto.status = "true";
                OtpValidation otpValidation = _context.OtpValidations.Where(x => x.email.Equals(request.userName)).FirstOrDefault();
                
                if(otpValidation != null)
                {
                    if(otpValidation.tokenTime.AddMinutes(otpValidation.tokenExpireTime) < DateTime.UtcNow)
                        _context.OtpValidations.Remove(otpValidation);
                    else
                        return responseDto;
                }
                else
                { 
                    var otpValidationEntity = new OtpValidation
                    {
                        user_id = user.Id,
                        email = request.userName,
                        token = GenerateNewRandom(),
                        tokenTime = DateTime.UtcNow,
                        tokenExpireTime = 15
                    };
                    _context.OtpValidations.Add(otpValidationEntity);
                }
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                responseDto.userName = user.Email;
                responseDto.status = "false";
            }
            
            return responseDto;
        }
        private static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }
    }
}
