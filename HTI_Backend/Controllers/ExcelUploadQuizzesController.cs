using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace HTI_Backend.Controllers
{

    public class ExcelUploadQuizzesController : ApiBaseController
    {
        private readonly IGenericRepository<Quiz> _quizRepository;

        public ExcelUploadQuizzesController(IGenericRepository<Quiz> quizRepository)
        {
            _quizRepository = quizRepository;
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
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[1];

                    for (int row = 2; row <= workSheet.Dimension.End.Row; row++)
                    {
                        for (int col = 1; col <= workSheet.Dimension.End.Column; col++)
                        {
                            var quiz = new Quiz
                            {
                                StudentCourseHistoryId = int.Parse(workSheet.Cells[row, 1].Value.ToString()),
                                QuizName = workSheet.Cells[1, col].Value.ToString(),
                                QuizDate = DateTime.Parse(workSheet.Cells[2, col].Value.ToString()),
                                QuizGrade = float.Parse(workSheet.Cells[row, col].Value.ToString())
                            };

                            await _quizRepository.AddAsync(quiz);
                        }
                    }
                }
            }

            return Ok("Quiz data uploaded successfully.");
        }
    }
}





















    