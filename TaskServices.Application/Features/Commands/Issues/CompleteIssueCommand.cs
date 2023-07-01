using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Issues
{
    public class CompleteIssueCommand : IRequest<List<Issue>>
    {
        public int SprintId { get; set; }
        public int SprintIdToUpdate { get; set; }
        public CompleteIssueCommand(int sprintId, int sprintIdToUpdate)
        {
            SprintId = sprintId;
            SprintIdToUpdate = sprintIdToUpdate;
        }
    }
}
