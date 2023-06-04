﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Queries.Issues
{
    public class GetIssueListQuery : IRequest<List<IssueDTO>>
    {
        public int Id { get; set; }
        public GetIssueListQuery(int id)
        {
            Id = id;
        }
    }
}
