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
        }
    }
}
