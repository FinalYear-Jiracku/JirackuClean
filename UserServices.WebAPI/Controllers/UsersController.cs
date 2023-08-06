using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using UserServices.Application.Features.Commands;
using UserServices.Application.Features.Queries;
using UserServices.Application.Interfaces.IServices;

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
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Email");
            var userId = claim.Subject.Claims.ToList()[0].Value;
            var user = await _mediator.Send(new GetProfileQueries(Int32.Parse(userId)));
            return Ok(user);
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
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
