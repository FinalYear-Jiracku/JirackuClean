using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;

namespace UserServices.Application.Features.Queries
{
    public class GetYearQuery : IRequest<List<YearDTO>>
    {
    }
}
