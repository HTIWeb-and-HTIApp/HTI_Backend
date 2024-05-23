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


            CreateMap<TimeLineCreateDTO, TimeLine>()
           
                ;

            CreateMap<TimeLineUpdateDTO, TimeLine>();
            CreateMap<TimeLine, TimeLineReturnDTO>() 
                .ForMember(d => d.CourseID , O => O.MapFrom( S=>S.Group.Course.CourseId ))
                .ForMember(d => d.CourseCode , O => O.MapFrom( S=>S.Group.Course.CourseCode ))
                ;

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
            CreateMap <Group,GroupStudentsReturnDTO>()
             .ForMember(d => d.CourseCode, o => o.MapFrom(s => s.Course.CourseCode))
            .ForMember(d => d.CourseName, o => o.MapFrom(s => s.Course.Name))
            .ForMember(d => d.CourseId, o => o.MapFrom(s => s.Course.CourseId))
            .ForMember(d => d.GroupId, o => o.MapFrom(s => s.GroupId))
            .ForMember(d => d.DoctorName, o => o.MapFrom(s => s.Doctor.Name))
            .ForMember(d => d.TeachingAssistantName, o => o.MapFrom(s => s.TeachingAssistant.Name))
        ;
            //  CreateMap<Group, DoctorCoursesReturnDto>()
            //      .ForMember(d => d.DoctorName, O => O.MapFrom(s => s.Doctor.Name))
            //      .ForMember(d => d.DoctorId, O => O.MapFrom(s => s.DoctorId))
            //      ;

             CreateMap<Course, coursesdTO>()
             .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
              .ForMember(d => d.CourseCode, o => o.MapFrom(s => s.CourseCode))
              .ForMember(d => d.MidtermGrades, o => o.MapFrom(s => s.StudentCourseHistories.Select(SCH => SCH.MidtermGrades).FirstOrDefault()))
              .ForMember(d => d.WorkGrades, o => o.MapFrom(s => s.StudentCourseHistories.Select(SCH => SCH.WorkGrades).FirstOrDefault()))
              .ForMember(d => d.FinalGrades, o => o.MapFrom(s => s.StudentCourseHistories.Select(SCH => SCH.FinalGrades).FirstOrDefault()))
              .ForMember(d => d.GradeLatter, o => o.MapFrom(s => s.StudentCourseHistories.Select(SCH => SCH.GradeLatter).FirstOrDefault()))

              ;

            //  CreateMap<GroupDto, DoctorCoursesReturnDto>()
            //.ForMember(d => d.DoctorId, o => o.MapFrom(s => s.DoctorId))
            //.ForMember(d => d.courses, o => o.MapFrom(s => s.Course));
           
            CreateMap<Course, course>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.CourseCode, o => o.MapFrom(s => s.CourseCode))
            .ForMember(d => d.GroupNumber, o => o.MapFrom(s => s.Groups.Select(s => s.GroupNumber).FirstOrDefault()))
            ;
            CreateMap<Doctor, DoctorCoursesReturnDto>()
                .ForMember(d => d.DoctorName, O => O.MapFrom(s => s.Name))
                .ForMember(d => d.DoctorId, O => O.MapFrom(s => s.DoctorId));      
            
            CreateMap<TeachingAssistant, TACoursesReturnDto>()
                .ForMember(d => d.Name, O => O.MapFrom(s => s.Name))
                .ForMember(d => d.TeachingAssistantId, O => O.MapFrom(s => s.TeachingAssistantId));
           
;


            CreateMap<Student, student>()
             .ForMember(d => d.StudentId, o => o.MapFrom(s => s.StudentId))
             .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Name));

            CreateMap<StudentCourseHistory, ResultReturnDTO>()
                .ForMember(dest => dest.courses, opt => opt.MapFrom(src => new List<Course> { src.Course }));

            CreateMap<StudentCourseHistory, StudentsLastTermCoursesDTOs>()
            .ForMember(d => d.CourseCode, o => o.MapFrom(s => s.Course.CourseCode))
            .ForMember(d => d.CourseName, o => o.MapFrom(s => s.Course.Name));


            CreateMap<Registration, ScheduleReturnDTO>()
             .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Group.Course.Name))
             .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.Group.Course.CourseCode))

             ;
        }

    }
}
//=