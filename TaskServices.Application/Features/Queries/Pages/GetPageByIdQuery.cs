﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Pages
{
    public class GetPageByIdQuery : IRequest<PageDTO>
    {
        public int Id { get; set; }
        public GetPageByIdQuery(int id)
        {
            Id = id;
        }
    }
}
