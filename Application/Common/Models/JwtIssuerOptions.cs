using Microsoft.IdentityModel.Tokens;
using System;

namespace Application.Common.Models
{
    public class JwtIssuerOptions
    {       
        public string issuer { get; set; }      
        public string subject { get; set; }       
        public string audience { get; set; }                
        public DateTime notBefore => DateTime.UtcNow;
        public DateTime issuedAt => DateTime.UtcNow;
        /// <summary>
        /// Set the timespan the token will be valid for (default is 5 min/300 seconds)
        /// </summary>
        public TimeSpan validFor { get; set; } = TimeSpan.FromSeconds(43200);
        public DateTime expiration => issuedAt.Add(validFor);
        public Func<string> JtiGenerator =>
          () => Guid.NewGuid().ToString();
        /// <summary>
        /// The signing key to use when generating tokens.
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
