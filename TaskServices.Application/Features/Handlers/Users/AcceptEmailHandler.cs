using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Columns;
using TaskServices.Application.Features.Queries.Users;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Application.Features.Handlers.Users
{
    public class AcceptEmailHandler : IRequestHandler<AcceptEmailQuery, int>
    {
        private readonly INotificationEventSubcriber _notificationEventSubcriber;
        public AcceptEmailHandler(INotificationEventSubcriber notificationEventSubcriber)
        {
            _notificationEventSubcriber = notificationEventSubcriber;
        }
        public async Task<int> Handle(AcceptEmailQuery query, CancellationToken cancellationToken)
        {
            await _notificationEventSubcriber.ReceiveMessage(query.InviteToken,query.InviteToken);
            return await Task.FromResult(0);
        }
    }
}
