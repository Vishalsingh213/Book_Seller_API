
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Common.Methods
{
    public class CommonMethods
    {
        private static IHttpContextAccessor _httpContextAccessor { get; set; }

        private static IConfigurationSection _jwtSettings { get; set; }
        public static string GetTokenDataModel(string reqToken, SigningCredentials signingCredentials)
        {
            //  _httpContextAccessor = request;
            TokenModel token = new TokenModel();
            var accessToken = string.Empty;
            string AccessToken = string.Empty;

            var authToken = reqToken;

            try
            {
                if (authToken != null)
                {
                    var encryptData = GetDataFromToken(authToken);

                    if (encryptData != null && encryptData.Claims != null)
                    {
                        AccessToken = GetRefreshToken(encryptData, signingCredentials);

                        //token = new TokenModel()
                        //{
                        //    accesstoken = AccessToken
                        //};
                        // token.request = result.re;
                        accessToken = AccessToken;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return accessToken;
        }

        public static string GetRefreshToken(dynamic encryptData, SigningCredentials signingCredentials)
        {
            JwtIssuerOptions _jwtOptions = new JwtIssuerOptions();
            //create claim for login user
            var claims = encryptData.Claims;


            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtOptions.issuer,
                audience: _jwtOptions.audience,
                claims: claims,
                expires: _jwtOptions.expiration,
                signingCredentials: signingCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return encodedJwt;
        }

        public static SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public static dynamic GetDataFromToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                return handler.ReadToken(token);
            }
            catch (Exception)
            {
                return null;
            }

        }
    }

    public class JwtDecodeWrapper : IJwtDecodeWrapper
    {
        private IHttpContextAccessor _httpContextAccessor { get; set; }

        public List<Claim> JwtTokenDecodeData(IHttpContextAccessor httpContextAccessor)
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
                //if (_httpContextAccessor.HttpContext.Request.Headers["token"].Count > 0)
                //    token = _httpContextAccessor.HttpContext.Request.Headers["token"][0]?.Split(" ")[1];

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

