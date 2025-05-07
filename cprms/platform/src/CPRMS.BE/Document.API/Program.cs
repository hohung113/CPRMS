using Core.Customized.CustomizePoints.DocumentCentrePoints.Interface;
using Core.Infrastructure.DIExtensions;
using Refit;

namespace Document.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //builder.Services.AddInfrastructureServices();
        builder.Services.AddRefitClient<IAuthorizationInterfaceClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://localhost:7107/rms/authserver");
            });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
