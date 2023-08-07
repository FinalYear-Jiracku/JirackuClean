using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Sprints
{
    public class StatisNumIssuesQuery : IRequest<List<StatisNumIssueDTO>>
    {
        public int Id { get; set; }
        public StatisNumIssuesQuery(int id)
        {
            Id = id;
        }
    }
}
