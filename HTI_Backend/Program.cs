using System.Text;
using Announcement;
using Azure.Storage.Blobs;
using HTI.Core;
using HTI.Core.RepositoriesContract;
using HTI.Repository;
using HTI.Repository.Data;
using HTI.Service;
using HTI_Backend.Errors;
using HTI_Backend.Helper;
using HTI_Backend.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            builder.Services.AddScoped(typeof(ItrainningResiteration), typeof(trainningResiteration));
            builder.Services.AddScoped(typeof(IGraduationRepository), typeof(GraduationRepository));
            builder.Services.AddScoped(typeof(IGraduationRepository), typeof(GraduationRepository));
            builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));

            //builder.Services.AddScoped(typeof(IdentityUser<string>), typeof(identityUser));

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
            builder.Services.AddScoped(_ =>
                new BlobServiceClient(builder.Configuration.GetConnectionString("AzureStorage")));
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
            var connectionString = builder.Configuration.GetConnectionString("AzureStorage");
            Console.WriteLine($"Azure Storage Connection String: {connectionString}");

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<StoreContext>();
            builder.Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // wallahy ma faker
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(Options =>
               {
                   Options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateAudience = true,
                       ValidAudience = builder.Configuration["JWT:ValidAudience"],
                       ValidateIssuer = true,
                       ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.FromDays(double.Parse(builder.Configuration["JWT:DurationInDays"]))
                   };
               });

            builder.Services.AddSignalR();
            builder.Services.AddSingleton<NotificationHub>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod().AllowCredentials();
                    });
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
                //await StoreContextSeed.SeedAsync (_dbcontext);  //  call data seeding
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                await StoreContextSeed.SeedAsync(_dbcontext, userManager);

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply migration");
            }
            #endregion


            #region Configure Kestral Middleres
            // Configure the HTTP request pipeline.
           
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin"); // Apply the CORS policy
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseMiddleware<ExceptionMiddleWare>();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("/notificationhub");
            });
            #endregion
            app.Run();
        }
    }
}