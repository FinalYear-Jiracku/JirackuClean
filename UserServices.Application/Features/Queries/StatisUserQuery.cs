using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;

namespace UserServices.Application.Features.Queries
{
    public class StatisUserQuery : IRequest<List<StatisUserDTO>>
    {
        public int? Year { get; set; }
        public StatisUserQuery(int? year)
        {
            Year = year;
        }
    }
}
