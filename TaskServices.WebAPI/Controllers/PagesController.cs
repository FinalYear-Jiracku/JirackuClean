using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.Features.Commands.Pages;
using TaskServices.Application.Features.Queries.Pages;
using TaskServices.Application.Features.Queries.Sprints;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET API
        [HttpGet]
        [Route("sprints/{id}")]
        public async Task<IActionResult> GetPageList(int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            var pageList = await _mediator.Send(new GetPageListQuery(id));
            return Ok(pageList);
        }

        [HttpGet]
        [Route("parent/{id}")]
        public async Task<IActionResult> GetChildPageList(int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            var pageList = await _mediator.Send(new GetChildPageListQuery(id));
            return Ok(pageList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPageDetail(int id)
        {
            var page = await _mediator.Send(new GetPageByIdQuery(id));
            if (page == null)
            {
                return StatusCode(400, "Page Does Not Exist");
            }
            return Ok(page);
        }
        #endregion

        #region POST API
        [HttpPost]
        public async Task<IActionResult> CreatePage([FromBody] CreatePageCommand pageCommand)
        {
            await _mediator.Send(pageCommand);
            return Ok();
        }
        #endregion

        #region PUT API
        [HttpPut]
        public async Task<IActionResult> UpdatePage([FromBody] UpdatePageCommand pageCommand)
        {
            await _mediator.Send(pageCommand);
            return Ok();
        }
        #endregion

        #region DELETE API
        [HttpPatch("{id}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            var findPage = await _mediator.Send(new GetPageByIdQuery(id));
            if (findPage == null)
            {
                return StatusCode(400, "Page Does Not Exist");
            }
            await _mediator.Send(new DeletePageCommand(id));
            return Ok();
        }
        #endregion
    }
}
