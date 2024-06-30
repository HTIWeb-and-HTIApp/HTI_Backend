using AutoMapper;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace HTI_Backend.Controllers
{
   
    public class GraduationProjectController : ApiBaseController
    {
        private readonly IGenericRepository<Student> _studentRepo;
        private readonly IMapper _mapper;

        public GraduationProjectController(IGenericRepository<Student> StudentRepo, IMapper mapper)
        {
            _studentRepo = StudentRepo;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(GraduationProjectReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]

        public async Task<IActionResult> GraduationProjectReport()
        {
            
            var students = await _studentRepo.FindByCondition(S => S.Credits >100);

            if (students is null) return NotFound(new ApiResponse(404));
            var mappedStudents = _mapper.Map<IEnumerable<Student>, IEnumerable<GraduationProjectReturnDTO>>(students);

            var report = new GraduationProjectReportDTO
            {
                StudentCount = mappedStudents.Count(),
                Students = mappedStudents
            };

            return Ok(report);
        }



        [HttpGet("report")]
        [ProducesResponseType(typeof(GraduationProjectReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GraduationProjectReport2()
        {
            var students = await _studentRepo.FindByCondition(S => S.Credits > 100);

            if (students is null)
                return NotFound(new ApiResponse(404));

            var mappedStudents = _mapper.Map<IEnumerable<Student>, IEnumerable<GraduationProjectReturnDTO>>(students);

            var report = new GraduationProjectReportDTO
            {
                StudentCount = mappedStudents.Count(),
                Students = mappedStudents
            };

            // Create the Excel file
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Graduation Project Report");

                // Set the column headers
                worksheet.Cells[1, 1].Value = "Student Name";
                worksheet.Cells[1, 2].Value = "Student ID";
                worksheet.Cells[1, 3].Value = "Credits";

                // Fill in the data
                int row = 2;
                foreach (var student in report.Students)
                {
                    worksheet.Cells[row, 1].Value = student.Name;
                    worksheet.Cells[row, 2].Value = student.StudentId;
                    worksheet.Cells[row, 3].Value = student.Credits;
                    row++;
                }

                // Auto-fit the columns
                worksheet.Cells.AutoFitColumns();

                // Convert the Excel package to a byte array
                byte[] fileBytes = package.GetAsByteArray();

                // Set the content type and file name for the response
                HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Response.Headers.Add("Content-Disposition", "attachment; filename=GraduationProjectReport.xlsx");

                // Return the Excel file as the response
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

    }
    public class GraduationProjectReportDTO
    {
        public int StudentCount { get; set; }
        public IEnumerable<GraduationProjectReturnDTO> Students { get; set; }
    }
}
