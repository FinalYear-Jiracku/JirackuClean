using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Projects
{
    public class UpgradeProjectCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
