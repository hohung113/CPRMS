using Autofac.Core;
using Core.Api.MediatRCustom;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rms.API.Middlewares;
using Rms.Application.Extensions;
using Rms.Application.Modules.UserManagement.CommandHandler;
using Rms.Domain.Context;
using Rms.Infrastructure.Extensions;
using System.Text;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        var AllowAllCorsPolicy = "_allowAllCorsPolicy";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: AllowAllCorsPolicy,
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader();
                              });
        });
        builder.Services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            //options.LowercaseQueryStrings = true;
        });
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateSemesterCommandHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
        });
  
        builder.Services.AddScoped<IDispatcher, Dispatcher>();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Rms.API",
                Description = "API for the CPRMS system",
                Contact = new OpenApiContact
                {
                    Name = "Contact with CPRMS Development",
                    Email = "cprm@gmail.com",
                    Url = new Uri("https://daihoc.fpt.edu.vn/")
                },
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. " +
                                "Enter your token in the format: Bearer {token}. " +
                                "Example: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"          
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddHttpContextAccessor();
        CPRMSHttpContext.Configure(builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>());
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options => {
            options.Events.OnRedirectToLogin = context =>
            {
                if (context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                }
                context.Response.Redirect(context.RedirectUri);
                return Task.CompletedTask;
            };
        })
        .AddGoogle(options =>
        {
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException("Google ClientId not configured.");
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException("Google ClientSecret not configured.");
            options.Scope.Add("email");
            options.Scope.Add("profile");
            options.SaveTokens = true;
        })
        .AddJwtBearer(options => 
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = builder.Environment.IsProduction();

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
        builder.Services.AddAuthorization();
        var app = builder.Build();
        // Seed Db & Auto migration data
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();
            try
            {
                var dbContext = services.GetRequiredService<RmsDbContext>();
                logger.LogInformation("Checking for pending migrations...");
                var pendingMigrations = dbContext.Database.GetPendingMigrations();

                if (pendingMigrations.Any())
                {

                    logger.LogInformation("Found {PendingMigrationsCount} pending migration(s). Applying...", pendingMigrations.Count());
                    dbContext.Database.Migrate();
                    logger.LogInformation("Database migrations applied successfully.");
                }
                else
                {
                    logger.LogInformation("Database is up to date. No pending migrations.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database at startup.");
            }
        }
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(c =>
            {
                c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
            });
            app.UseSwaggerUI();
        }
     
        app.UseExceptionHandler("/error");
        app.UseMiddleware<CampusResolutionMiddleware>();
        app.UseGlobalExceptionHandlerMiddleware();
        //app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<JwtAuthenticationMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(AllowAllCorsPolicy);
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
