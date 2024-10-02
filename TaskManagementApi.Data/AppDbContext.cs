using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data.Entities;

namespace TaskManagementApi.Data;

public class AppDbContext : DbContext
{
    private const string UserProjectTableName = "UserProject";
    
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Entities.Task> Tasks { get; set; }

    public string DbPath { get; set; }

    public AppDbContext()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        DbPath = Path.Join(path, "api.db"); 
    }

    // TODO Extract to appsettings.json
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
                    .HasMany(u => u.AssignedProjects)
                    .WithMany(p => p.AssignedUsers);

        modelBuilder.Entity<Project>()
                    .HasOne(p => p.Owner)
                    .WithMany(u => u.OwnedProjects)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Entities.Task>()
                    .HasOne(t => t.AssignedUser)
                    .WithMany(u => u.AssignedTasks)
                    .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Entities.Task>()
                    .HasOne(t => t.ReporterUser)
                    .WithMany(u => u.ReportedTasks)
                    .OnDelete(DeleteBehavior.Restrict);
    }
}