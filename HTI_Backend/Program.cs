using HTI.Core.RepositoriesContract;
using HTI.Repository;
using HTI.Repository.Data;
using HTI_Backend.Errors;
using HTI_Backend.Helper;
using HTI_Backend.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HTI_Backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Cofigure Services
            // Add services to the container.
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IRegistrationRepository), typeof(RegistrationRepository));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                    .SelectMany(p => p.Value.Errors).Select(E => E.ErrorMessage).ToList();
                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); 

            builder.Services.AddDbContext<StoreContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });
            #endregion

            var app = builder.Build();

            #region UPDATE-DATABASE
            using var scope = app.Services.CreateScope();
            //Group of services life time scooped

            var services = scope.ServiceProvider;
            //services it self


            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
               // ask clr for create an objext from dbcontext exiplictly

               var _dbcontext = services.GetRequiredService<StoreContext>();
                await _dbcontext.Database.MigrateAsync(); //update database

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply migration");
            }
            #endregion


            #region Configure Kestral Middleres
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion
            app.Run();
        }
    }
}