using Api.Controllers;
using Application.LoginCQ;
using Application.LoginCQ.Command;
using Application.LoginCQ.Querries;
using Application.LoginCQ.ViewModel;
using Core.Entitites;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Application.Common.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ApiControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        public LoginController(ILogger<LoginController> logger, IConfiguration configuration, IApplicationDbContext context)
        {
            _logger = logger;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(logger));
            _jwtSettings = _configuration.GetSection("JWT");
            _context = context;
        }


        
        
        [HttpPost("login")]
        public async Task<LoginResponseDto> login([FromBody] Login query)
        {
            try
            {
                return await Mediator.Send(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
            
        }

        [HttpPost("twoFactorAuthentication")]
        public async Task<IActionResult> TwoFactorAuthentication([FromBody] OtpValidationCommand command)
        {
            
            try
            {
                string token;
                OTPResponseDto oTPResponseDto =  await Mediator.Send(command);
                if(oTPResponseDto.status == true)
                {
                    token = await GenerateToken(oTPResponseDto.userId, command.email);
                    if(token != null)
                    {
                        var userUserLoggedInStatus = Mediator.Send(new UpdateUserLoggedInStatusQuery.UpdateUserLoggedInStatusCommand
                        {
                            email = command.email
                        });
                    }
                    return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token, UserId = oTPResponseDto.userId });
                }
                else
                {
                    return BadRequest("Invalid token verification");
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }

        }


        [NonAction]
        public async Task<string> GenerateToken(long userId, string email)
        {
            try
            {
                var signingCredentials = GetSigningCredentials();
                var claims = await GetRoleClaims(userId, email);
                var tokenOptions = new JwtSecurityToken(
                    issuer: _jwtSettings.GetSection("validIssuer").Value,
                    audience: _jwtSettings.GetSection("validAudience").Value,
                    claims: claims,
                    expires: DateTime.UtcNow.AddSeconds(Convert.ToInt32(_jwtSettings.GetSection("expiryInSeconds").Value)),
                    signingCredentials: signingCredentials);
                string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return token;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [NonAction]
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        [NonAction]
        private async Task<List<Claim>> GetRoleClaims(long userId, string email)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim("email", email),
                    new Claim("UserId", userId.ToString())
                };
                var roles = await _context.UserRoles.Where(x => x.user_id == userId).ToListAsync();

                foreach (var role in roles)
                {
                    var roleName = await _context.Roles.Where(x => x.role_Id == role.id).ToListAsync();
                    var roleToClaim = await _context.RoleToCalims.Where(x => x.role_Id == role.role_id).ToListAsync();

                    foreach(var item in roleName)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.Name));
                    }
                    foreach(var item in roleToClaim)
                    {
                        claims.Add(new Claim(item.claim_type.ToString(), item.claim_value.ToString()));
                    }
                }
                return claims;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
