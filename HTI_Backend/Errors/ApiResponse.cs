using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HTI_Backend.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Massage { get; set; }
        public ApiResponse(int statusCode, string? massage = null)
        {
            StatusCode = statusCode;
            Massage = massage ?? GetDefultMassageForStatusCode(StatusCode);
        }

        private string? GetDefultMassageForStatusCode(int statusCode)
        {
            return StatusCode switch
            {
                400 => "Bad Requst",
                401 => "You are not Authorized",
                404 => "Resouce not Found",
                500 => "Internal Server Error",
                _ => null,
            };  
        }
    }
}
