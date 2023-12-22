using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserServices.Application.Features.Commands.Payment;
using UserServices.Application.Features.Queries;
using UserServices.Application.Interfaces.IServices;

namespace UserServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentIntentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentIntentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> PaymentIntent([FromBody] PaymentIntentCommand command)
        {
            var paymentIntent = await _mediator.Send(command);
            return Ok(paymentIntent);
        }
    }
}
