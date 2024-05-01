using HTI_Backend.Errors;
using System.Text.Json;

namespace HTI_Backend.Middlewares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate Next ,ILogger<ExceptionMiddleWare> logger ,IHostEnvironment env)
        {
            _next = Next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next.Invoke(context);
                
            }catch (Exception ex)
            {
                _logger.LogError(ex ,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                //if (_env.IsDevelopment())
                //{
                //    var Response = new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString());
                //}
                //else
                //{
                //    var Response = new ApiExceptionResponse(500);
                //}
                var Response = _env.IsDevelopment() ? new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionResponse(500);
                var Oprions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var JsonResponse = JsonSerializer.Serialize(Response , Oprions);
                context.Response.WriteAsync(JsonResponse);
            }
        }
    }
}
