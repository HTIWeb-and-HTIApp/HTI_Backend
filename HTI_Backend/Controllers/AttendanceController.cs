using AutoMapper;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HTI_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IGenericRepository<Attendance> _attendanceRepo;
        private readonly IGenericRepository<Registration> _registrationRepo;
        private readonly IGenericRepository<Group> _groupRepo;
        private readonly IMapper _mapper;

        public AttendanceController(IGenericRepository<Attendance> attendanceRepo, IGenericRepository<Registration> registrationRepo, IGenericRepository<Group> groupRepo, IMapper mapper)
        {
            _attendanceRepo = attendanceRepo;
            _registrationRepo = registrationRepo;
            _groupRepo = groupRepo;
            _mapper = mapper;
        }

        [HttpGet("generate-qr")]
        public async Task<IActionResult> GenerateQRCode(int groupId, string courseCode, int weekNumber, bool isLecture, double longitude, double latitude)
        {
            // Fetch the group
            var group = await _groupRepo.GetByIdAsync(groupId);
            if (group == null)
            {
                return NotFound(new { Message = "Group not found." });
            }

            // Set QR code as active
            group.IsQRCodeActive = true;
            await _groupRepo.UpdateAsync(group);

            var qrText = $"https://university.com/attendance?groupId={groupId}&courseCode={courseCode}&weekNumber={weekNumber}&isLecture={isLecture}&longitude={longitude}&latitude={latitude}";

            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCoder.QRCode(qrCodeData);
                using (var bitmap = qrCode.GetGraphic(4))
                {
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        var qrCodeBytes = ms.ToArray();
                        return File(qrCodeBytes, "image/png");
                    }
                }
            }
        }

        [HttpPost("record-attendance")]
        public async Task<IActionResult> RecordAttendance(int studentId, int groupId, string courseCode, int weekNumber, bool isLecture, double studentLongitude, double studentLatitude , double DoctorLongitude, double DoctorLatitude)
        {
            // Fetch the group
            var group = await _groupRepo.GetByIdAsync(groupId);
            if (group == null)
            {
                return NotFound(new { Message = "Group not found." });
            }

            // Check if the QR code is active
            if (!group.IsQRCodeActive)
            {
                return BadRequest(new { Message = "QR code is not valid." });
            }

            // Check if the student is registered in the group
            var registration = await _registrationRepo.GetAllAsync();
            var validRegistration = registration.Any(r => r.StudentId == studentId && r.GroupId == groupId && r.IsOpen);

            if (!validRegistration)
            {
                return BadRequest(new { Message = "Student is not registered in this group." });
            }

            // Check if attendance has already been recorded for this student, group, course, and week
            var existingAttendance = await _attendanceRepo.GetAllAsync();
            var alreadyRecorded = existingAttendance.Any(a => a.StudentId == studentId && a.GroupId == groupId && a.CourseCode == courseCode && a.WeekNumber == weekNumber && a.AttendanceType == isLecture);

            if (alreadyRecorded)
            {
                return BadRequest(new { Message = "Attendance has already been recorded." });
            }

            // Calculate the distance between the QR code location and the student's location
            double distance = GetDistance(studentLatitude, studentLongitude, DoctorLongitude, DoctorLatitude);

            // Allowable distance in meters (e.g., within 50 meters)
            double allowableDistance = 50;

            if (distance > allowableDistance)
            {
                return BadRequest(new { Message = "You are not in the required location to record attendance." });
            }

            var attendance = new Attendance
            {
                StudentId = studentId,
                GroupId = groupId,
                CourseCode = courseCode,
                WeekNumber = weekNumber,
                AttendanceType = isLecture, // True for lecture, False for section
                AttendanceDate = DateTime.UtcNow,
                Longitude = studentLongitude,
                Latitude = studentLatitude,
            };

            await _attendanceRepo.AddAsync(attendance);

            return Ok(new { Message = "Attendance recorded successfully." });
        }

        [HttpPost("deactivate-qr")]
        public async Task<IActionResult> DeactivateQRCode(int groupId)
        {
            var group = await _groupRepo.GetByIdAsync(groupId);

            if (group == null)
            {
                return NotFound(new { Message = "Group not found." });
            }

            if (!group.IsQRCodeActive)
            {
                return BadRequest(new { Message = "QR code is already deactivated." });
            }

            group.IsQRCodeActive = false;
            await _groupRepo.UpdateAsync(group);

            return Ok(new { Message = "QR code deactivated successfully." });
        }

        // Haversine formula to calculate the distance between two points on the Earth's surface
        private double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371e3; // Earth radius in meters
            double phi1 = lat1 * Math.PI / 180;
            double phi2 = lat2 * Math.PI / 180;
            double deltaPhi = (lat2 - lat1) * Math.PI / 180;
            double deltaLambda = (lon2 - lon1) * Math.PI / 180;

            double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                       Math.Cos(phi1) * Math.Cos(phi2) *
                       Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = R * c; // Distance in meters
            return distance;
        }
    }
}
