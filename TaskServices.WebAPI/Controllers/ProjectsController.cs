using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Projects;
using TaskServices.Application.Features.Queries.Projects;   
using TaskServices.Domain.Entities;
using TaskServices.Shared.Pagination.Filter;
using TaskServices.Shared.Pagination.Helpers;
using TaskServices.Shared.Pagination.Uris;

namespace TaskServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUriService _uriService;
        public ProjectsController(IMediator mediator, IUriService uriService)
        {
            _mediator = mediator;
            _uriService = uriService;
        }

        #region GET API
        [HttpGet]
        public async Task<IActionResult> GetProjectList([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var projectList = await _mediator.Send(new GetProjectListQuery(filter));
            var pagedResponse = PaginationHelper.CreatePagedReponse<ProjectDTO>(projectList.Item1, projectList.Item2, projectList.Item3, _uriService, route);
            return Ok(pagedResponse);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProjectDetail(int id)
        //{
        //    var project = await _mediator.Send(new GetProjectByIdQuery(id));
        //    if (project == null)
        //    {
        //        return StatusCode(400, "Project Does Not Exist");
        //    }
        //    return Ok(project);
        //}
        #endregion

        #region POST API
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand projectCommand)
        {
            //var checkProjectName = await _mediator.Send(new CheckProjectNameCommand(project.Id, project.Name));
            //if (checkProjectName)
            //{
            //    return StatusCode(400, "Project Name already Exist");
            //}
            var newProject = await _mediator.Send(projectCommand);
            return Ok(newProject);
        }
        #endregion

        #region PUT API
        //[HttpPut]
        //public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectCommand projectCommand)
        //{
        //    var checkProjectName = await _mediator.Send(new CheckProjectNameCommand(project.Id, project.Name));
        //    if (checkProjectName)
        //    {
        //        return StatusCode(400, "Project Name already Exist");
        //    }
        //    var newProject = await _mediator.Send(projectCommand);
        //    return Ok(newProject);
        //}
        #endregion

        #region DELETE API
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProject(int id)
        //{
        //    var findProject = await _mediator.Send(new GetProjectByIdQuery(id));
        //    if (findProject == null)
        //    {
        //        return StatusCode(400, "Project Does Not Exist");
        //    }
        //    await _mediator.Send(new DeleteProjectCommand(id));
        //    return Ok();
        //}
        #endregion
    }
}
