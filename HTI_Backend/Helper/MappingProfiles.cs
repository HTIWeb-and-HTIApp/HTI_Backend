using AutoMapper;
using HTI.Core.Entities;
using HTI_Backend.DTOs;

namespace HTI_Backend.Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Student, StudentToReturnDTO>()
                .ForMember(d => d.Department ,O => O.MapFrom(S => S.Department.DepartmentName));
            CreateMap<Student, AllStudentReturnDTO>()
                .ForMember(d => d.Department, O => O.MapFrom(S => S.Department.DepartmentName));
            CreateMap<Course, AllCoursesReturnDTO>()
                .ForMember(d => d.Department, O => O.MapFrom(S => S.Department.DepartmentName));


            // ana (ahmed) 3malt dooooool
            CreateMap<Group, GroupReturnDTO>();

            CreateMap<GroupCreateDTO, Group>();

            CreateMap<Group, GroupUpdateDTO>();
            CreateMap<GroupUpdateDTO, Group>();



            CreateMap<Registration, StudentCoursesRetuenDTOs>();



        }

    }
}
