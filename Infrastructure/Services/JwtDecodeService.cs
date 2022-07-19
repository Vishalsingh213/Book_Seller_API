using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Innoid.Infrastructure.Services
{
    public static class JwtDecodeService
    {
        private static IHttpContextAccessor _httpContextAccessor { get; set; }

        public static List<Claim> JwtTokenDecodeData(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request != null)
            {
                string token = null;
                if (_httpContextAccessor.HttpContext.Request.Headers["token"].Count > 0)
                {
                    var data = _httpContextAccessor.HttpContext.Request.Headers["token"][0].Split(" ").ToList();
                    if (data != null && data.Count > 1)
                        token = _httpContextAccessor.HttpContext.Request.Headers["token"][0]?.Split(" ")[1];
                }
                   

                if (token == "null" || token == null)
                {
                    if (_httpContextAccessor.HttpContext.Request.Headers["Authorization"].Count > 0)
                        token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"][0]?.Split(" ")[1];
                }
                if (token == "null" || token == null)
                {
                    if (_httpContextAccessor.HttpContext.Request.Headers["HeaderAuthorization"].Count > 0)
                        token = _httpContextAccessor.HttpContext.Request.Headers["HeaderAuthorization"][0]?.Split(" ")[1];
                }
                JwtSecurityToken tokenInfo = new JwtSecurityToken();
                if (token != "null" && token != null)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var readtoken = handler.ReadToken(token);
                    tokenInfo = readtoken as JwtSecurityToken;
                    return tokenInfo.Claims.ToList();
                }

                return new List<Claim>();
            }

            return null;
        }
    }
}
