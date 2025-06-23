
using GZY.Quartz.MUI.EFContext;
using GZY.Quartz.MUI.Extensions;
using Microsoft.EntityFrameworkCore;
using NineSun.Quartz.Web.Domain.Conf;
using MySqlConnector;
using NineSun.Quartz.Web.Domain;
using NineSun.Quartz.Web.Domain.DB;
using NLog;
using NLog.Web;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpLogging;

namespace NineSun.Quartz.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            //应用程序启动
            logger.Info("Application startup");

            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            //清空日志供应程序，避免.net自带日志输出到命令台
            builder.Logging.ClearProviders();
            //使用NLog日志
            builder.Host.UseNLog();

            builder.Services.AddHttpLogging(options =>
            {
                options.LoggingFields = HttpLoggingFields.ResponsePropertiesAndHeaders | HttpLoggingFields.RequestPropertiesAndHeaders;
                options.CombineLogs = true;
            });

          
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NineSun API", Version = "v1" });
            });
            //builder.Services.AddQuartzUI();

            var optionsBuilder = new DbContextOptionsBuilder<QuarzEFContext>();
            var serverVersion = new MySqlServerVersion(new Version(5, 5, 27));
            //quartz db
            var dbconfs = configuration.GetSection("DbConfig").Get<List<DbConfigItem>>();
            
            //db
            optionsBuilder.UseMySql(dbconfs.First(x => x.Key == "quartz").ConnectionString, serverVersion);
            builder.Services.AddQuartzUI(optionsBuilder.Options);
            builder.Services.AddSingleton(new DatabaseConn(dbconfs));  

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

            // 启用HTTP请求日志记录
            app.UseHttpLogging();

            app.UseAuthorization();
            app.UseQuartzUIBasicAuthorized();
            app.UseQuartz();

            app.MapControllers();

            app.Run();
        }
    }
}
