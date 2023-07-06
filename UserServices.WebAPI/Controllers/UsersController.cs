using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using UserServices.Application.Features.Commands;
using UserServices.Application.Features.Queries;

namespace UserServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.Email);
            var userId = claim.Subject.Claims.ToList()[0];
            var user = _mediator.Send(new GetProfileQueries(Int32.Parse(userId.Value)));
            if (user == null)
            {
                return StatusCode(401, "User can not Found");
            }
            var expires = claim.Subject.Claims.ToList()[3];
            var exp = JsonConvert.DeserializeObject<object>(expires.Value.ToString());
            var result = new { User = user.Result, Exp = exp };
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
