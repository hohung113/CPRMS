using Auth.API.Infrastructure.Persistence;
using Core.CPRMSServiceComponents.Middlewares;
using Microsoft.EntityFrameworkCore;


namespace Auth.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        var connectionString = builder.Configuration.GetConnectionString("CprmsDb");
        builder.Services.AddDbContext<AuthDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        var app = builder.Build();
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler("/error");

        //app.UseGlobalExceptionHandlerMiddleware();
        //app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
