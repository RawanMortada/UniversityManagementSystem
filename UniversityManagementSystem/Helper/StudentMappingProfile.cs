using AutoMapper;
using UniversityManagementSystem.Dto.StudentDto;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Helper
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Student, GetAllStudentsDto>();
            CreateMap<GetAllStudentsDto, Student>();

            CreateMap<Student, GetStudentByIdDto>();
            CreateMap<GetStudentByIdDto, Student>();

            CreateMap<Student, AddStudentDto>();
            CreateMap<AddStudentDto, Student>();

            CreateMap<Student, UpdateStudentDto>();
            CreateMap<UpdateStudentDto, Student>();

        }
    }
}
