using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace HTI_Backend.Controllers
{

    public class ExcelUploadResultController : ApiBaseController
    {
        private readonly IGenericRepository<StudentCourseHistory> _studentCourseHistoryRepository;

        public ExcelUploadResultController(IGenericRepository<StudentCourseHistory> studentCourseHistoryRepository)
        {
            _studentCourseHistoryRepository = studentCourseHistoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcelFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                    for (int row = 2; row <= workSheet.Dimension.End.Row; row++)
                    {
                        int studentId = int.Parse(workSheet.Cells[row, 1].Value.ToString());
                        int courseId = int.Parse(workSheet.Cells[row, 2].Value.ToString());
                        int groupId = int.Parse(workSheet.Cells[row, 3].Value.ToString());
                        int doctorId = int.Parse(workSheet.Cells[row, 4].Value.ToString());
                        int teachingAssistantId = int.Parse(workSheet.Cells[row, 5].Value.ToString());
                        float gpa = float.Parse(workSheet.Cells[row, 6].Value.ToString());
                        float workGrades = float.Parse(workSheet.Cells[row, 7].Value.ToString());
                        float finalGrades = float.Parse(workSheet.Cells[row, 8].Value.ToString());
                        float midtermGrades = float.Parse(workSheet.Cells[row, 9].Value.ToString());
                        bool status = bool.Parse(workSheet.Cells[row, 10].Value.ToString());

                        // Check if a record already exists
                        var existingRecord = await _studentCourseHistoryRepository.GetAsync(s => s.StudentId == studentId && s.CourseId == courseId && s.GroupId == groupId);

                        if (existingRecord != null)
                        {
                            // Update existing record
                            existingRecord.DoctorId = doctorId;
                            existingRecord.TeachingAssistantId = teachingAssistantId;
                            existingRecord.GPA = gpa;
                            existingRecord.WorkGrades = workGrades;
                            existingRecord.FinalGrades = finalGrades;
                            existingRecord.MidtermGrades = midtermGrades;
                            existingRecord.Status = status;

                            await _studentCourseHistoryRepository.UpdateAsync(existingRecord);
                        }
                        else
                        {
                            // Add new record
                            var studentCourseHistory = new StudentCourseHistory
                            {
                                StudentId = studentId,
                                CourseId = courseId,
                                GroupId = groupId,
                                DoctorId = doctorId,
                                TeachingAssistantId = teachingAssistantId,
                                GPA = gpa,
                                WorkGrades = workGrades,
                                FinalGrades = finalGrades,
                                MidtermGrades = midtermGrades,
                                Status = status
                            };

                            await _studentCourseHistoryRepository.AddAsync(studentCourseHistory);
                        }
                    }
                }
            }

            return Ok("Data uploaded successfully.");
        }
    }
}
