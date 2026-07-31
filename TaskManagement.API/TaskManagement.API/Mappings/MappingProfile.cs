using AutoMapper;
using TaskManagement.API.DTOModels.Auth;
using TaskManagement.API.DTOModels.Project;
using TaskManagement.API.DTOModels.Task;
using TaskManagement.API.DTOModels.User;
using TaskManagement.API.Models;

namespace TaskManagement.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Mappings
            CreateMap<User, UserDTO>();

            // Auth Mappings
            CreateMap<RegisterRequestDTO, User>();
            CreateMap<User, LoginResponseDTO>();

            // Project Mappings
            CreateMap<CreateProjectDTO, Project>();
            CreateMap<UpdateProjectDTO, Project>();
            CreateMap<Project, ProjectResponseDTO>()
                .ForMember(
                    dest => dest.CreatedByName,
                    opt => opt.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.Name : string.Empty)
                );

            // Task Mappings
            CreateMap<CreateTaskDto, TaskItem>();
            CreateMap<UpdateTaskDto, TaskItem>();
            CreateMap<TaskItem, TaskResponseDto>()
                .ForMember(
                    dest => dest.ProjectName,
                    opt => opt.MapFrom(src => src.Project != null ? src.Project.Name : string.Empty)
                )
                .ForMember(
                    dest => dest.AssignedUserName,
                    opt => opt.MapFrom(src => src.AssignedUser != null ? src.AssignedUser.Name : null)
                );
        }
    }
}