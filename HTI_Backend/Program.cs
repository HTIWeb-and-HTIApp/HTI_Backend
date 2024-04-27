using HTI.Repository.Data;
using Microsoft.AspNetCore.Identity;
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
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion
            app.Run();
        }
    }
}