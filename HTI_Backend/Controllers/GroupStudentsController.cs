using AutoMapper;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using Group = HTI.Core.Entities.Group;
using System.IO;

namespace HTI_Backend.Controllers
{
    public class GroupStudentsController : ApiBaseController
    {
        private readonly IGenericRepository<Group> _groupsRepo;
        private readonly IMapper _mapper;

        public GroupStudentsController(IMapper mapper, IGenericRepository<Group> groupsRepo)
        {
            _mapper = mapper;
            _groupsRepo = groupsRepo;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<GroupStudentsReturnDTO>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetAllStudentReqInGroups(int id)
        {
            var group = _groupsRepo.FindByCondition(w => w.GroupId == id, g => g.Include(g => g.Course).Include(w => w.Registrations).ThenInclude(w => w.Student).Include(w => w.Doctor).Include(w => w.TeachingAssistant)).Result.FirstOrDefault();
            if (group == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var groupdto = _mapper.Map<GroupStudentsReturnDTO>(group);
            groupdto.Studs = _mapper.Map<List<student>>(group.Registrations.Select(e => e.Student).ToList());

            // Generate Excel file
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Students");

            // Add headers
            worksheet.Cells[1, 1].Value = "Group ID";
            worksheet.Cells[1, 2].Value = "Course ID";
            worksheet.Cells[1, 3].Value = "Course Code";
            worksheet.Cells[1, 4].Value = "Course Number";
            worksheet.Cells[1, 5].Value = "Course Name";
            worksheet.Cells[1, 6].Value = "Doctor Name";
            worksheet.Cells[1, 7].Value = "Teaching Assistant Name";
            worksheet.Cells[1, 8].Value = "Student ID";
            worksheet.Cells[1, 9].Value = "Student Name";

            // Add group data
            worksheet.Cells[2, 1].Value = groupdto.GroupId;
            worksheet.Cells[2, 2].Value = groupdto.CourseId;
            worksheet.Cells[2, 3].Value = groupdto.CourseCode;
            worksheet.Cells[2, 4].Value = groupdto.GroupNumber;
            worksheet.Cells[2, 5].Value = groupdto.CourseName;
            worksheet.Cells[2, 6].Value = groupdto.DoctorName;
            worksheet.Cells[2, 7].Value = groupdto.TeachingAssistantName;

            // Add student data
            var rowIndex = 2;
            foreach (var student in groupdto.Studs)
            {
                worksheet.Cells[rowIndex, 7].Value = student.StudentId;
                worksheet.Cells[rowIndex, 8].Value = student.StudentName;
                rowIndex++;
            }

            // Convert the Excel package to a byte array
            var fileContent = package.GetAsByteArray();

            // Return the Excel file
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GroupStudents.xlsx");
        }
    }
}
