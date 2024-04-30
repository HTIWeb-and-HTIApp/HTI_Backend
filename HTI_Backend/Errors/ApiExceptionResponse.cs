namespace HTI_Backend.Errors
{
    public class ApiExceptionResponse :ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int StausCode , string? Massage = null,string? details = null):base(StausCode , Massage)
        {
            Details = details;
        }
    }
}
