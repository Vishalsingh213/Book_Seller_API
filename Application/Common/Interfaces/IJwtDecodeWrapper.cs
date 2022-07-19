using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Common.Interfaces
{
    public interface IJwtDecodeWrapper
    {
        List<Claim> JwtTokenDecodeData(IHttpContextAccessor httpContextAccessor);
    }
}
