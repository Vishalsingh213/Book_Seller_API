using Api.Controllers;
using Application.DashBoardCQ.Querry;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ApiControllerBase
    {

        [HttpGet("getDashBoard")]
        public async Task<IActionResult> getDashboard([FromQuery] GetDasboardUrlQuery query)
        {
            try
            {
                Mediator.Send(query);
            }
            catch(Exception  ex)
            {
                Mediator.Send(ex);
            }
            return Ok();
        }
    }
}
