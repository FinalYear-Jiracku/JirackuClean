using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Columns;
using TaskServices.Application.Features.Queries.Columns;
using TaskServices.Application.Features.Queries.Sprints;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ColumnsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET API
        [HttpGet]
        [Route("data/sprints/{id}")]
        public async Task<IActionResult> GetDataColumnList(int id)
        {
            var findSprint = await _mediator.Send(new GetSprintByIdQuery(id));
            if (findSprint == null)
            {
                return StatusCode(400, "Sprint Does Not Exist");
            }
            var columnList = await _mediator.Send(new GetDataColumnListQuery(id));
            return Ok(columnList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetColumnDetail(int id)
        {
            var column = await _mediator.Send(new GetColumnByIdQuery(id));
            if (column == null)
            {
                return StatusCode(400, "Column Does Not Exist");
            }
            return Ok(column);
        }
        #endregion

        #region POST API
        [HttpPost]
        public async Task<IActionResult> CreateColumn([FromBody] CreateColumnCommand columnCommand)
        {
            var checkColumnName = await _mediator.Send(new CheckColumnNameQuery(columnCommand.Name));
            if (checkColumnName)
            {
                return StatusCode(400, "Column Name already Exist");
            }
            await _mediator.Send(columnCommand);
            return Ok();
        }
        #endregion

        #region PUT API
        [HttpPut]
        public async Task<IActionResult> UpdateColumn([FromBody] UpdateColumnCommand columnCommand)
        {
            var checkColumnName = await _mediator.Send(new CheckColumnNameQuery(columnCommand.Id, columnCommand.Name));
            if (checkColumnName)
            {
                return StatusCode(400, "Column Name already Exist");
            }
            await _mediator.Send(columnCommand);
            return Ok();
        }
        #endregion

        #region DELETE API
        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteColumn(int id)
        {
            var findColumn = await _mediator.Send(new GetColumnByIdQuery(id));
            if (findColumn == null)
            {
                return StatusCode(400, "Column Does Not Exist");
            }
            await _mediator.Send(new DeleteColumnCommand(id));
            return Ok();
        }
        #endregion
    }
}
