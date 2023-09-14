using AutoMapper;
using DMAWS_T2204M_NguyeDuyQuoc.DTOs;
using DMAWS_T2204M_NguyeDuyQuoc.Entities;

namespace DMAWS_T2204M_NguyeDuyQuoc
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.ProjectEmployees, opt => opt.MapFrom(src => src.ProjectEmployees));
            CreateMap<ProjectDTO, ProjectDTO>();


            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.ProjectEmployees, opt => opt.MapFrom(src => src.ProjectEmployees));
            CreateMap<EmployeeDTO, Employee>();


        }
    }
}
