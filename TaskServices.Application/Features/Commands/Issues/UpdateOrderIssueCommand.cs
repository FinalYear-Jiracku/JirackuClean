using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.Features.Commands.Issues
{
    public class UpdateOrderIssueCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int Order { get; set; }
        public UpdateOrderIssueCommand(int id, int statusId, int order)
        {
            Id = id;
            StatusId = statusId;
            Order = order;
        }
    }
}
