using AutoMapper;
using UniversityManagementSystem.Dto.StudentCourseDto;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Helper
{
    public class StudentCourseMappingProfile : Profile
    {
        public StudentCourseMappingProfile()
        {
            CreateMap<StudentCourse, GetAllStudentCourseDto>();
            CreateMap<GetAllStudentCourseDto, StudentCourse>();

            CreateMap<StudentCourse, GetStudentCourseByIdDto>();
            CreateMap<GetStudentCourseByIdDto, StudentCourse>();

            CreateMap<StudentCourse, AddStudentCourseDto>();
            CreateMap<AddStudentCourseDto, StudentCourse>();

            CreateMap<StudentCourse, UpdateStudentCourseDto>();
            CreateMap<UpdateStudentCourseDto, StudentCourse>();
        }
    }
}
