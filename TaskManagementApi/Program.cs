using TaskManagementApi.Data;
using TaskManagementApi.Data.Repositories;
using TaskManagementApi.Services.ProjectHandling;
using TaskManagementApi.Services.TaskHandling;
using TaskManagementApi.Services.UserHandling;

namespace TaskManagementApi;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));

        builder.Services.AddScoped<AppDbContext>();

        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<ProjectRepository>();
        builder.Services.AddScoped<TaskRepository>();
        
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<ITaskService, TaskService>();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}