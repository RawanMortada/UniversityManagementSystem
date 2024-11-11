using AutoMapper;
using UniversityManagementSystem.Dto.CourseDto;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Helper
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<Course, GetAllDto>();
            CreateMap<GetAllDto, Course>();

            CreateMap<Course, GetByIdDto>();
            CreateMap<GetByIdDto, Course>();

            CreateMap<Course, AddCourseDto>();
            CreateMap<AddCourseDto, Course>();

            CreateMap<Course, UpdateCourseDto>();
            CreateMap<UpdateCourseDto, Course>();
        }
    }
}
