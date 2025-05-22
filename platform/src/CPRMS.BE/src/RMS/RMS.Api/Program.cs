using Core.Api.MediatRCustom;
using Core.Api.ServiceComponents.JWTService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rms.Application.Modules.UserManagement.QueryHandler;
using Rms.Domain.Modules.UserSystem.Interface;
using Rms.Infrastructure.Extensions;
using Rms.Infrastructure.Modules.UserSystem.Repository;
using System.Text;
using Microsoft.Extensions.Logging;
using Mapster;
using Rms.Application.Extensions;
using Rms.Application.Modules.Acedamic.CommandHandler;
using Autofac.Core;
using Rms.Application.Modules.Acedamic.Validator;
using Core.Api.Middlewares;
using Rms.Application.Modules.UserManagement.CommandHandler;
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
        // builder.Services.AddMapster(); 
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateSemesterCommandHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
        });
  
        builder.Services.AddScoped<IDispatcher, Dispatcher>();
        //builder.Services.AddMapster();
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
                    Email = "hunghpvde170589@fpt.edu.com",
                    Url = new Uri("https://www.facebook.com/id130203")
                },
                // License = new OpenApiLicense
                // {
                //     Name = " MIT",
                //     Url = new Uri("https://opensource.org/licenses/MIT")
                // }
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

        builder.Services.AddScoped<TokenService>();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication(builder.Configuration);

        builder.Services.Configure<AccountSettings>(builder.Configuration.GetSection("Account"));
        builder.Services.Configure<RmsSystemConfig>(builder.Configuration.GetSection("RmsSystem"));
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
        // Google - JWT - Cookie Config
        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //})
        //.AddCookie()
        //.AddGoogle(options =>
        //{
        //    options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException("Google ClientId not configured.");
        //    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException("Google ClientSecret not configured.");
        //    options.Scope.Add("email");
        //    options.Scope.Add("profile");
        //    //options.ClaimActions.MapJsonKey("urn:google:email", "email", "string");
        //    options.SaveTokens = true;
        //})
        //.AddJwtBearer(options =>
        //{
        //    options.SaveToken = true;
        //    options.RequireHttpsMetadata = builder.Environment.IsProduction(); // True cho production

        //    options.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //        ValidateAudience = true,
        //        ValidAudience = builder.Configuration["Jwt:Audience"],
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        //        ValidateLifetime = true,
        //        ClockSkew = TimeSpan.Zero
        //    };
        //});
        builder.Services.AddAuthentication(options =>
        {
            // Khi một [Authorize] attribute được sử dụng mà không chỉ định scheme,
            // hãy cố gắng xác thực bằng JWT Bearer trước tiên.
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            // Nếu xác thực JWT thất bại cho một API và cần "challenge",
            // hãy sử dụng challenge của JWT Bearer (thường trả về 401 Unauthorized).
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            // Đối với luồng đăng nhập Google (OAuth), khi Google xác thực xong và
            // ứng dụng của bạn cần "đăng nhập" người dùng vào hệ thống (thường là qua cookie tạm thời),
            // hãy sử dụng scheme Cookie.
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            // Bạn có thể đặt DefaultScheme là JWT nếu phần lớn ứng dụng của bạn là API.
            // Hoặc có thể bỏ qua nếu DefaultAuthenticateScheme và DefaultChallengeScheme đã rõ ràng.
            // options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
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
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Rms.API v1");
            //    // options.RoutePrefix = string.Empty;
            //});
        }
     
        app.UseExceptionHandler("/error");
        //app.UseMiddleware<TenantResolutionMiddleware>();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(AllowAllCorsPolicy);
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
