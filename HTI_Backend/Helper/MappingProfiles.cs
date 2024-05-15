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
            CreateMap<Student, GraduationProjectReturnDTO>();
            CreateMap<Student, LastTermReturnDTO>();
            CreateMap<StudentCourseHistory, StudentOpenReqCoursesReturnDTO>()
                                .ForMember(d => d.CourseCode, O => O.MapFrom(S => S.Group.Course.CourseCode))
                                .ForMember(d => d.Name, O => O.MapFrom(S => S.Group.Course.Name))
                                .ForMember(d => d.Credits, O => O.MapFrom(S => S.Group.Course.Credits))
                                .ForMember(d => d.Description, O => O.MapFrom(S => S.Group.Course.Description))
                                .ForMember(d => d.Links, O => O.MapFrom(S => S.Group.Course.Links))
                                .ForMember(d => d.StudyYear, O => O.MapFrom(S => S.Group.Course.StudyYear))
                                .ForMember(d => d.Semester, O => O.MapFrom(S => S.Group.Course.Semester))
                                .ForMember(d => d.Lap, O => O.MapFrom(S => S.Group.Course.Lap))
                                .ForMember(d => d.Type, O => O.MapFrom(S => S.Group.Course.Type))
                                .ForMember(d => d.PrerequisiteId, O => O.MapFrom(S => S.Group.Course.PrerequisiteId));
                //.
                //ForMember(d => d.Status , O => O.MapFrom(S =>S.StudentCourseHistories.));




            CreateMap<Registration, StudentCoursesRetuenDTOs>()
                                .ForMember(d => d.CourseCode, O => O.MapFrom(S => S.Group.Course.CourseCode))
                                .ForMember(d => d.Name, O => O.MapFrom(S => S.Group.Course.Name))
                                .ForMember(d => d.Credits, O => O.MapFrom(S => S.Group.Course.Credits))
                                .ForMember(d => d.Description, O => O.MapFrom(S => S.Group.Course.Description))
                                .ForMember(d => d.Links, O => O.MapFrom(S => S.Group.Course.Links))
                                .ForMember(d => d.StudyYear, O => O.MapFrom(S => S.Group.Course.StudyYear))
                                .ForMember(d => d.Semester, O => O.MapFrom(S => S.Group.Course.Semester))
                                .ForMember(d => d.Lap, O => O.MapFrom(S => S.Group.Course.Lap))
                                .ForMember(d => d.Type, O => O.MapFrom(S => S.Group.Course.Type))
                                .ForMember(d => d.PrerequisiteId, O => O.MapFrom(S => S.Group.Course.PrerequisiteId))
                                .ForMember(d => d.DepartmentId, O => O.MapFrom(S => S.Group.Course.DepartmentId))
                                .ForMember(d => d.DoctorId , O => O.MapFrom(S => S.Group.DoctorId))
                                .ForMember(d => d.DoctorName, O => O.MapFrom(S => S.Group.Doctor.Name))
                                .ForMember(d => d.GroupNumber, O => O.MapFrom(S => S.Group.GroupNumber))
                                ;


            CreateMap<Group, GroupStudentsReturnDTO>()
                        .ForMember(d => d.CourseCode, o => o.MapFrom(s => s.Course.CourseCode))
                        .ForMember(d => d.CourseName, o => o.MapFrom(s => s.Course.Name))
                        .ForMember(d => d.CourseId, o => o.MapFrom(s => s.Course.CourseId))
                        .ForMember(d => d.GroupId, o => o.MapFrom(s => s.GroupId))
                        .ForMember(d => d.DoctorName, o => o.MapFrom(s => s.Doctor.Name))
                        .ForMember(d => d.TeachingAssistantName, o => o.MapFrom(s => s.TeachingAssistant.Name))
                    ;







            CreateMap<StudentCourseHistory, StudentsLastTermCoursesDTOs>()
            .ForMember(d => d.CourseCode, o => o.MapFrom(s => s.Course.CourseCode))
            .ForMember(d => d.CourseName, o => o.MapFrom(s => s.Course.Name))
            .ForMember(d => d.StudentCount, o => o.MapFrom(s => s.Course.StudentCourseHistories.Count()));   



            CreateMap<Student, student>()
             .ForMember(d => d.StudentId, o => o.MapFrom(s => s.StudentId))
             .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Name));





        }

    }
}
