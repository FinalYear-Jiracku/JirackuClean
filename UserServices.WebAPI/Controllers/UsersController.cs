using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using UserServices.Application.DTOs;
using UserServices.Application.Features.Commands;
using UserServices.Application.Features.Commands.User;
using UserServices.Application.Features.Queries;
using UserServices.Shared.Pagination.Filter;
using UserServices.Shared.Pagination.Helpers;
using UserServices.Shared.Pagination.Uris;

namespace UserServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUriService _uriService;
        public UsersController(IMediator mediator, IUriService uriService)
        {
            _mediator = mediator;
            _uriService = uriService;
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetAllUser([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var userList = await _mediator.Send(new GetListUserQuery(filter));
            var pagedResponse = PaginationHelper.CreatePagedReponse<UserDTO>(userList.Item1, userList.Item2, userList.Item3, _uriService, route);
            return Ok(pagedResponse);
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Email");
            var userId = claim.Subject.Claims.ToList()[0].Value;
            var user = await _mediator.Send(new GetProfileQuery(Int32.Parse(userId)));
            return Ok(user);
        }
        [HttpGet("years")]
        public async Task<IActionResult> GetYear()
        {
            return Ok(await _mediator.Send(new GetYearQuery()));
        }
        [HttpGet("statis")]
        public async Task<IActionResult> StatisUser([FromQuery] int year)
        {
            return Ok(await _mediator.Send(new StatisUserQuery(year)));
        }
        [HttpPost("admin/login")]
        public async Task<IActionResult> Login([FromBody] LoginAdminCommand command)
        {
            return Ok(await _mediator.Send(command));
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
        [HttpPatch("disable/{id}")]
        public async Task<IActionResult> DisableUser(int id)
        {
            await _mediator.Send(new DisableUserCommand(id));
            return Ok();
        }
        [HttpPatch("enable/{id}")]
        public async Task<IActionResult> EnableUser(int id)
        {
            await _mediator.Send(new EnableUserCommand(id));
            return Ok();
        }
    }
}
