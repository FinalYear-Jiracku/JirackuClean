using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Application.Features.Commands
{
    public class EnableUserCommand : IRequest<int>
    {
        public int Id { get; set; }
        public EnableUserCommand(int id)
        {
            Id = id;
        }
    }
}
