using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace HTI_Backend.Controllers
{
    
    public class StudentLastTermController : ApiBaseController
    {
        private readonly IGenericRepository<Student> _studentRepo;
        private readonly IMapper _mapper;
        private readonly IConverter _pdfConverter;

        public StudentLastTermController(IGenericRepository<Student> StudentRepo, IMapper mapper, IConverter pdfConverter)
        {
            _studentRepo = StudentRepo;
            _mapper = mapper;
            _pdfConverter = pdfConverter;

        }
        [HttpGet]
        [ProducesResponseType(typeof(LastTermReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> LastTermReport()
        {

            var students = await _studentRepo.FindByCondition(S => S.Credits > 137);

            if (students is null) return NotFound(new ApiResponse(404));
            var mappedStudents = _mapper.Map<IEnumerable<Student>, IEnumerable<LastTermReturnDTO>>(students);

            var report = new LastTermDto
            {
                StudentCount = mappedStudents.Count(),
                Students = mappedStudents
            };

            return Ok(report);
        }

        [HttpGet("last-term-report")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LastTermReportt()
        {
            var students = await _studentRepo.FindByCondition(s => s.Credits > 137);

            if (students == null || !students.Any())
                return NotFound(new ApiResponse(404));

            var mappedStudents = _mapper.Map<IEnumerable<Student>, IEnumerable<LastTermReturnDTO>>(students);

            var report = new LastTermDto
            {
                StudentCount = mappedStudents.Count(),
                Students = mappedStudents
            };


            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Header().Text("Last Term Student Report").FontSize(20);
                    page.Margin(50);

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Student ID").Bold();
                            header.Cell().Text("Name").Bold();
                            header.Cell().Text("Credits").Bold();
                        });

                        foreach (var student in report.Students)
                        {
                            table.Cell().Element(CellStyle).Text(student.StudentId.ToString());
                            table.Cell().Element(CellStyle).Text(student.Name);
                            table.Cell().Element(CellStyle).Text(student.Credits.ToString());
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
                });
            });

            // Generate PDF bytes
            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", "LastTermReport.pdf");
        }
        private static IContainer CellStyle(IContainer container)
        {
            return container.DefaultTextStyle(x => x.FontSize(12)).PaddingVertical(5);
        }


        [HttpGet("last-term-reporttttt")]
        public async Task<IActionResult> LastTermReporttt()
        {
            // Create a new PDF document
            var students = await _studentRepo.FindByCondition(s => s.Credits > 137);

            if (students == null || !students.Any())
                return NotFound(new ApiResponse(404));

            var mappedStudents = _mapper.Map<IEnumerable<Student>, IEnumerable<LastTermReturnDTO>>(students);

            using (var document = new PdfDocument())
            {
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);

                var fontTitle = new XFont("Arial", 20, XFontStyle.Bold);
                var fontData = new XFont("Arial", 12, XFontStyle.Regular);

                var yPosition = 50; // Initial y-position for title

                // Draw title
                gfx.DrawString("Last Term Report", fontTitle, XBrushes.Black,
                    new XRect(0, yPosition, page.Width, 50), XStringFormats.TopCenter);

                yPosition += 70; // Move down for student data

                // Draw student data
                foreach (var student in mappedStudents)
                {
                    gfx.DrawString($"Name: {student.Name}, Credits: {student.Credits}", fontData,
                        XBrushes.Black, new XRect(50, yPosition, page.Width - 100, 20),
                        XStringFormats.TopLeft);
                    yPosition += 20; // Increment y-position for next student data
                }

                using (var stream = new MemoryStream())
                {
                    document.Save(stream, false);
                    return File(stream.ToArray(), "application/pdf", "LastTermReport.pdf");
                }
            }
        }







    }
    public class LastTermDto
    {
        public int StudentCount { get; set; }
        public IEnumerable<LastTermReturnDTO> Students { get; set; }
    }
}

