using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Queries.Users
{
    public class AcceptEmailQuery : IRequest<int>
    {
        public string? InviteToken { get; set; }
        public AcceptEmailQuery(string inviteToken)
        {
            InviteToken = inviteToken;
        }
    }
}
