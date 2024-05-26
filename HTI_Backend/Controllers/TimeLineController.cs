using AutoMapper;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Sas;
using Azure.Storage.Blobs.Models;
using System.Text.RegularExpressions;
using HTI.Repository.Data;

namespace HTI_Backend.Controllers
{

    public class TimeLineController : ApiBaseController
    {
        private readonly IGenericRepository<TimeLine> _timeLineRepo;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly StoreContext _dbContext;
        private readonly IGenericRepository<Registration> _regRepo;

        public TimeLineController(IGenericRepository<TimeLine> timeLineRepo, IMapper mapper, BlobServiceClient blobServiceClient,
            StoreContext dbContext, IGenericRepository<Registration> regRepo)
        {
            _timeLineRepo = timeLineRepo;
            _mapper = mapper;
            _blobServiceClient = blobServiceClient;
            _dbContext = dbContext;
            _regRepo = regRepo;
        }
        // Create a new timeline

        [HttpPost]
        [ProducesResponseType(typeof(TimeLineReturnDTO), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> CreateTimeLine([FromForm] TimeLineCreateDTO timeLineCreateDTO, [FromForm] List<IFormFile> Files)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));
            var timeline = _mapper.Map<TimeLine>(timeLineCreateDTO);


            if (Files != null && Files.Count > 0)
            {
                foreach (var file in Files)
                {
                    var fileUrl = await UploadBlobAsync(file, "tasks");
                    timeline.Files.Add(new TimeLineFile
                    {
                        OriginalFileName = file.FileName,
                        BlobName = Path.GetFileName(fileUrl),
                        ContentType = file.ContentType,
                        SasUrl = fileUrl
                    });
                }
            }
            _dbContext.TimeLines.Add(timeline);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTimeLine), new { id = timeline.TimeLineId }, _mapper.Map<TimeLineReturnDTO>(timeline));

        }



        private async Task<string> UploadBlobAsync(IFormFile file, string containerName)
        {
            var blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var blobClient = _blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);

            await blobClient.UploadAsync(file.OpenReadStream(), new BlobHttpHeaders { ContentType = file.ContentType });

            return await GenerateSasUrlAsync(blobClient);
        }

        // SAS URL Generation Method
        private async Task<string> GenerateSasUrlAsync(BlobClient blobClient)
        {
            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = blobClient.BlobContainerName,
                BlobName = blobClient.Name,
                Resource = "b",  // Resource type: blob
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMonths(6) // SAS valid for 24 hours
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
            return sasUri.ToString();
        }






        // Create multiple timelines


        [HttpPost("bulk")]
        [ProducesResponseType(typeof(IEnumerable<TimeLineReturnDTO>), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> CreateTimeLines([FromBody] IEnumerable<TimeLineCreateDTO> timeLineCreateDTOs)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));

            var timeLines = _mapper.Map<IEnumerable<TimeLineCreateDTO>, IEnumerable<TimeLine>>(timeLineCreateDTOs);
            await _timeLineRepo.AddRangeAsync(timeLines);

            return Ok();

        }
        // Get a timeline by ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TimeLineReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<TimeLine>> GetTimeLine(int id)
        {

            var timeline = await _dbContext.TimeLines
            .Include(ni => ni.Files)
            .FirstOrDefaultAsync(ni => ni.TimeLineId == id);

            if (timeline == null)
                return NotFound();
            var timeLineReturnDTO = _mapper.Map<TimeLineReturnDTO>(timeline);

            return Ok(timeLineReturnDTO);
        }
        // Get a timeline by GroupID

        [HttpGet("group/{groupId}")]
        [ProducesResponseType(typeof(IEnumerable<TimeLineReturnDTO>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<IEnumerable<TimeLineReturnDTO>>> GetTimeLinesByGroupId(int groupId)
        {
            var timelines = await _dbContext.TimeLines
                .Include(ni => ni.Files)
                .Where(ni => ni.GroupId == groupId)
                .ToListAsync();

            if (!timelines.Any())
                return NotFound();

            var timeLineReturnDTOs = _mapper.Map<IEnumerable<TimeLine>, IEnumerable<TimeLineReturnDTO>>(timelines);
            return Ok(timeLineReturnDTOs);
        }


        // Get all timelines
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TimeLineReturnDTO>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetAllTimeLines()
        {
            var timeLines = await _dbContext.TimeLines
                    .Include(t => t.Files) // Eagerly load files
                    .ToListAsync();
            if (!timeLines.Any()) return NotFound(new ApiResponse(404));

            var timeLineReturnDTOs = _mapper.Map<IEnumerable<TimeLine>, IEnumerable<TimeLineReturnDTO>>(timeLines);
            return Ok(timeLineReturnDTOs);
        }

        // Update a timeline
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TimeLineReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> UpdateTimeLine(int id, [FromBody] TimeLineUpdateDTO timeLineUpdateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));

            var timeLines = await _timeLineRepo.FindByCondition(t => t.TimeLineId == id , S => S.Include(T => T.Group).ThenInclude(T => T.Course));
            if (!timeLines.Any()) return NotFound(new ApiResponse(404));

            var timeLine = timeLines.First();
            _mapper.Map(timeLineUpdateDTO, timeLine);
            await _timeLineRepo.UpdateAsync(timeLine);

            var timeLineReturnDTO = _mapper.Map<TimeLine, TimeLineReturnDTO>(timeLine);
            return Ok(timeLineReturnDTO);
        }   
        // Delete a timeline
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), 204)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> DeleteTimeLine(int id)
        {
            var timeLines = await _timeLineRepo.FindByCondition(t => t.TimeLineId == id);
            if (!timeLines.Any()) return NotFound(new ApiResponse(404));

            var timeLine = timeLines.First();
            await _timeLineRepo.DeleteAsync(timeLine);
            return NoContent();
        }


        // get all timelines for a student

        [HttpGet("MyCoursesWithTimeLines/{studentId}")]
        [ProducesResponseType(typeof(StudentCoursesWithTimeLinesDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetCoursesWithTimeLines(int studentId)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));

            var registrations = await _regRepo.FindByCondition(
                r => r.StudentId == studentId && r.IsOpen,
                include: q => q
                    .Include(r => r.Group)
                    .ThenInclude(g => g.Course)
                    .Include(r => r.Group.TimeLines)
                    .ThenInclude(tl => tl.Files)
            );

            // Filter registrations to include only those with timelines
            var coursesWithTimeLines = registrations
                .Where(r => r.Group.TimeLines.Any())
                .Select(r => new CourseWithTimeLineDTO
                {
                    CourseId = r.Group.Course.CourseId,
                    CourseCode = r.Group.Course.CourseCode,
                    Name = r.Group.Course.Name,
                    TimeLines = r.Group.TimeLines.Select(tl => new TimeLineReturnDTO
                    {
                        TimeLineId = tl.TimeLineId,
                        GroupId = tl.GroupId,
                        Type = tl.Type,
                        Title = tl.Title,
                        Description = tl.Description,
                        Deadline = tl.Deadline,
                        Files = tl.Files.Select(f => new TimeLineFileDTO
                        {
                            Id = f.Id,
                            OriginalFileName = f.OriginalFileName,
                            ContentType = f.ContentType,
                            SasUrl = f.SasUrl
                        }).ToList()
                    }).ToList()
                }).ToList();

            if (!coursesWithTimeLines.Any()) return NotFound(new ApiResponse(404));

            var result = new StudentCoursesWithTimeLinesDTO
            {
                StudentId = studentId,
                Courses = coursesWithTimeLines
            };

            return Ok(result);
        }




    }
}
