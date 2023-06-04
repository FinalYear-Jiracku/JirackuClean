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
            CreateMap<Project, ProjectDTO>().ForMember(dest => dest.Sprints, opt => opt.MapFrom(src => src.Sprints.Count())).ReverseMap();
            CreateMap<Sprint, SprintDTO>().ReverseMap();
            CreateMap<Status, StatusDTO>().ReverseMap();
            CreateMap<Status, DataStatusDTO>().ForMember(dest => dest.Issues, opt => opt.MapFrom(src => src.Issues)).ReverseMap();
            CreateMap<Issue, IssueDTO>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                                        .ForMember(dest => dest.SprintName, opt => opt.MapFrom(src => src.Sprint.Name))
                                        .ForMember(dest => dest.SubIssues, opt => opt.MapFrom(src => src.SubIssues.Count()))
                                        .ForMember(dest => dest.UserIssues, opt => opt.MapFrom(src => src.UserIssues))
                                        .ReverseMap();
            CreateMap<Issue, DataIssueDTO>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                                            .ForMember(dest => dest.SprintName, opt => opt.MapFrom(src => src.Sprint.Name))
                                            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments))
                                            .ForMember(dest => dest.SubIssues, opt => opt.MapFrom(src => src.SubIssues))
                                            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                                            .ForMember(dest => dest.UserIssues, opt => opt.MapFrom(src => src.UserIssues))
                                            .ReverseMap();
        }
    }
}
