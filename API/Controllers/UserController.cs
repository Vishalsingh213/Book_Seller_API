using Api.Controllers;
using Application.Common.Interfaces;
using Application.UserCQ.Query;
using Application.UserCQ.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly IApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        public UserController(ILogger<LoginController> logger, IConfiguration configuration, IApplicationDbContext context)
        {
            _logger = logger;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(logger));
            _jwtSettings = _configuration.GetSection("JWT");
            _context = context;
        }

        [HttpGet("getUserDetails")]
        public async Task<UserResponseDto> getUserBydetailId([FromQuery] GetUserDetailById request)
        {
            try
            {
                return await Mediator.Send(request);
            }
            catch (Exception ex)
            {/*
                _logger.LogError(ex, ex.Message);*/
                throw new Exception(ex.Message);
            }


        }

    }
}
