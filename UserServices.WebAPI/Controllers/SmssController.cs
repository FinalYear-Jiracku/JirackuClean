using AutoMapper.Internal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Stripe;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using UserServices.Application.Features.Commands;

namespace UserServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmssController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SmssController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("generate")]
        //[EnableRateLimiting("fixed")]
        public async Task<IActionResult> Generate([FromBody]SendSmsCodeCommand  command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("verify")]
        [DisableRateLimiting]
        public async Task<IActionResult> Verify([FromBody] VerifySmsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPut("disable/{id}")]
        [DisableRateLimiting]
        public async Task<IActionResult> Disable(int id)
        {
            return Ok(await _mediator.Send(new DisableCodeSmsCommand(id)));
        }
    }
}
