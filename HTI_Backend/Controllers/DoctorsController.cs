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

    public class DoctorsController : ApiBaseController
    {
        private readonly IGenericRepository<Doctor> _doctorRepo;
        private readonly IMapper _mapper;


        public DoctorsController(IGenericRepository<Doctor> DoctorRepo, IMapper mapper)
        {
            _doctorRepo = DoctorRepo;
            _mapper = mapper;
        }


        [HttpGet("{email}")]
        [ProducesResponseType(typeof(DoctorsDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetDoctorByEmail(string email)
        {
            var doctors = await _doctorRepo.FindByCondition(S => S.Email == email);
            var str = doctors.FirstOrDefault();
            if (str is null) return NotFound(new ApiResponse(404));
            var MappeedDoctor = _mapper.Map<Doctor, DoctorsDto>(str);
            return Ok(MappeedDoctor);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(DoctorsDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetAllDoctor()
        {
            var doctors = await _doctorRepo.FindByCondition(g => true);
            if (doctors is null) return NotFound(new ApiResponse(404));
            var MappeedDoctor = _mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorsDto> >(doctors);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Doctors");
                worksheet.Cells.LoadFromCollection(MappeedDoctor, true);

                var stream = new MemoryStream(package.GetAsByteArray());
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Doctors.xlsx";

                return File(stream, contentType, fileName);
            }
        }


    }
}
