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
            CreateMap<Issue, IssueDTO>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                                        .ForMember(dest => dest.Sprint, opt => opt.MapFrom(src => src.Sprint))
                                        .ForMember(dest => dest.SubIssues, opt => opt.MapFrom(src => src.SubIssues.Count()))
                                        .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                                        .ReverseMap();
            CreateMap<Issue, DeadlineIssuesDTO>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                                                 .ForMember(dest => dest.Sprint, opt => opt.MapFrom(src => src.Sprint.Name))
                                                 .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Email))
                                                 .ReverseMap();
            CreateMap<SubIssue, DeadlineSubIssuesDTO>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                                                   .ForMember(dest => dest.Issue, opt => opt.MapFrom(src => src.Issue.Name))
                                                   .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Email))
                                                   .ReverseMap();
            CreateMap<Issue, DataIssueDTO>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                                            .ForMember(dest => dest.Sprint, opt => opt.MapFrom(src => src.Sprint))
                                            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments))
                                            .ForMember(dest => dest.SubIssues, opt => opt.MapFrom(src => src.SubIssues))
                                            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                                            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                                            .ReverseMap();
            CreateMap<SubIssue, SubIssueDTO>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                                              .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                                              .ReverseMap();
            CreateMap<SubIssue, DataSubIssueDTO>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                                                  .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments))
                                                  .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                                                  .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                                                  .ReverseMap();
            CreateMap<Column, ColumnDTO>().ReverseMap();
            CreateMap<Column, DataColumnDTO>().ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes)).ReverseMap();
            CreateMap<Note, NoteDTO>().ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments)).ReverseMap();
            CreateMap<Comment, CommentDTO>().ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)).ReverseMap();
            CreateMap<Page, PageDTO>().ReverseMap();
            CreateMap<Attachment, AttachmentDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
