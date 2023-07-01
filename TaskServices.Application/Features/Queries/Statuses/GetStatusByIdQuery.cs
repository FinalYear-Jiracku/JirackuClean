﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Statuses
{
    public class GetStatusByIdQuery : IRequest<StatusDTO>
    {
        public int Id { get; set; }
        public GetStatusByIdQuery(int id)
        {
            Id = id;
        }
    }
}
