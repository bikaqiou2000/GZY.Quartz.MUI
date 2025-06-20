
using GZY.Quartz.MUI.EFContext;
using GZY.Quartz.MUI.Extensions;
using Microsoft.EntityFrameworkCore;
using NineSun.Quartz.Web.Domain.Conf;

namespace NineSun.Quartz.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddQuartzUI();

            var optionsBuilder = new DbContextOptionsBuilder<QuarzEFContext>();
            var serverVersion = new MySqlServerVersion(new Version(5, 5, 27));

            //quartz db
            var dbconfs = configuration.GetSection("DbConfig").Get<List<DbConfigItem>>();
            //db
            optionsBuilder.UseMySql(dbconfs.First(x => x.Key == "quartz").ConnectionString, serverVersion);
            builder.Services.AddQuartzUI(optionsBuilder.Options);

            var app = builder.Build();

            //confirm create database
            using (var scope = app.Services.CreateScope())
            {
                var qctx = scope.ServiceProvider.GetRequiredService<QuarzEFContext>();
                qctx.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseQuartzUIBasicAuthorized();
            app.UseQuartz();
            
            app.MapControllers();

            app.Run();
        }
    }
}
