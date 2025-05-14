using Core.Api.MediatRCustom;
using Core.Api.Middlewares;
using Core.Application.ServiceModel;
using Core.CPRMSServiceComponents.ServiceComponents.JWTService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.IdentityModel.Tokens;
using Rms.Infrastructure.Extensions;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            //options.LowercaseQueryStrings = true;
        });
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(IDispatcher).Assembly);
            //cfg.RegisterServicesFromAssembly(typeof().Assembly);
        });
        builder.Services.AddScoped<IDispatcher, Dispatcher>();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<TokenService>();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.Configure<AccountSettings>(builder.Configuration.GetSection("Account"));
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
        // Google - JWT - Cookie Config
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddGoogle(options =>
        {
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException("Google ClientId not configured.");
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException("Google ClientSecret not configured.");
            options.Scope.Add("email");
            options.Scope.Add("profile");
            //options.ClaimActions.MapJsonKey("urn:google:email", "email", "string");
            options.SaveTokens = true;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured.")))
            };
        });

        builder.Services.AddAuthorization();
        var app = builder.Build();
        // Seed Db
        //using (var scope = app.Services.CreateScope())
        //{
        //    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        //    dbContext.Database.EnsureCreated();
        //}

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler("/error");
        app.UseMiddleware<TenantResolutionMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("AllowAll");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
