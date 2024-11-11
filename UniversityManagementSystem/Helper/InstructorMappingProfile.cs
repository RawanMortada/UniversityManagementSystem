using AutoMapper;
using UniversityManagementSystem.Dto.CourseDto;
using UniversityManagementSystem.Dto.InstructorDto;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Helper
{
    public class InstructorMappingProfile : Profile
    {
        public InstructorMappingProfile() 
        {
            CreateMap<Instructor, GetAllInstructorsDto>();
            CreateMap<GetAllInstructorsDto, Instructor>();

            CreateMap<Instructor, GetInstructorByIdDto>();
            CreateMap<GetInstructorByIdDto, Instructor>();

            CreateMap<Instructor, AddInstructorDto>();
            CreateMap<AddInstructorDto, Instructor>();

            CreateMap<Instructor, UpdateInstructorDto>();
            CreateMap<UpdateInstructorDto, Instructor>();
        }
    }
}


