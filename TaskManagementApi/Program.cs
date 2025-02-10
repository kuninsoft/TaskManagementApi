using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using TaskManagementApi.Data;
using TaskManagementApi.Data.Repositories;
using TaskManagementApi.Services.LoginHandling;
using TaskManagementApi.Services.ProjectHandling;
using TaskManagementApi.Services.TaskHandling;
using TaskManagementApi.Services.UserHandling;
using TaskManagementApi.Swagger;

namespace TaskManagementApi;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        AddServices(builder);

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        builder.Services.Configure<DatabaseOptions>(
            builder.Configuration.GetSection(DatabaseOptions.DbConfiguration));
        builder.Services.AddScoped<AppDbContext>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddTransient<ILoginService, LoginService>();

        builder.Services
               .AddAuthentication(x=>
               {
                   x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                   x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                   x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidIssuer = builder.Configuration["JWT:Issuer"],
                       ValidAudience = builder.Configuration["JWT:ValidAudience"],
                       IssuerSigningKey = GetJwtSecurityKey(builder.Configuration["JWT:SecurityKey"]),
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                   };
               });
        
        builder.Services.AddAuthorization();
    }
    
    private static SymmetricSecurityKey GetJwtSecurityKey(string? key)
    {
        if (key is null)
        {
            throw new InvalidOperationException("Missing JWT Security Key");
        }

        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
}