using Api.Controllers;
using Application.DashBoardCQ.Querry;
using Application.DashBoardCQ.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ApiControllerBase
    {
        [HttpPost("getDashBoard")]
        public async Task<QuickSightURLDto> getDashboard([FromBody] GetDasboardUrlQuery query)
        {
            try
            {
                return await Mediator.Send(query);
            }
            catch(Exception  ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("getDashBoardDetails")]
        public async Task<List<QuickSightDashboardDetailDto>> getDashboard([FromQuery] GetDashBoardDetails query)
        {
            try
            {
                return await Mediator.Send(query);
            }
            catch(Exception  ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
