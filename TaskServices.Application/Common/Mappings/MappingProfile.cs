using AutoMapper;

using System.Reflection;
using TaskServices.Application.DTOs;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDTO>().ReverseMap();
        }
    }
}
