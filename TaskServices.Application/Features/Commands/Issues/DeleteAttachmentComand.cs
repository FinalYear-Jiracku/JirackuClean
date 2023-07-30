using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.Features.Commands.Issues
{
    public class DeleteAttachmentComand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteAttachmentComand(int id)
        {
            Id = id;
        }
    }
}
