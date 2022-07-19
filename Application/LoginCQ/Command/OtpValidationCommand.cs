using Application.Common.Interfaces;
using Application.LoginCQ.ViewModel;
using AutoMapper;
using Core.Entitites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LoginCQ.Command
{
    public class OtpValidationCommand : IRequest<OTPResponseDto>
    {
        public string email { get; set; }
        public string token { get; set; }
    }
    public class OtpValidationCommandHandler : IRequestHandler<OtpValidationCommand, OTPResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        OTPResponseDto oTPResponseDto;

        public OtpValidationCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            oTPResponseDto = new OTPResponseDto();
        }

        public async Task<OTPResponseDto> Handle(OtpValidationCommand request, CancellationToken cancellationToken)
        {
            User user = _context.Users.Where(x => x.Email.Equals(request.email)).FirstOrDefault();   //get user 
            
            if (user != null)
            {
                oTPResponseDto.userId = user.Id;
                OtpValidation otp = _context.OtpValidations.Where(x => x.email.Equals(request.email)).FirstOrDefault();
                if (otp.token.Equals(request.token) && otp.tokenTime.AddMinutes(otp.tokenExpireTime) > DateTime.UtcNow)
                {
                    oTPResponseDto.status = true;
                    _context.OtpValidations.Remove(otp);
                }
                else
                {
                    _context.OtpValidations.Remove(otp);
                    oTPResponseDto.status = false;
                }
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                oTPResponseDto.status = false;
            }
            
            return oTPResponseDto;

        }
    }
}
