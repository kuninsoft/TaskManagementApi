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
        modelBuilder.Entity<User>()
                    .HasMany(u => u.AssignedProjects)
                    .WithMany(p => p.AssignedUsers);
        
        modelBuilder.Entity<User>()
                    .HasIndex(u => u.Email)
                    .IsUnique();

        modelBuilder.Entity<Project>()
                    .HasOne(p => p.Owner)
                    .WithMany(u => u.OwnedProjects)
                    .HasForeignKey(p => p.OwnerId)
                    .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Project>()
                    .HasIndex(p => p.Name)
                    .IsUnique();

        modelBuilder.Entity<Entities.Task>()
                    .HasOne(t => t.AssignedUser)
                    .WithMany(u => u.AssignedTasks)
                    .HasForeignKey(t => t.AssignedUserId)
                    .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Entities.Task>()
                    .HasOne(t => t.ReporterUser)
                    .WithMany(u => u.ReportedTasks)
                    .HasForeignKey(t => t.ReporterUserId)
                    .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Project>()
                    .HasMany(p => p.Tasks)
                    .WithOne(t => t.Project)
                    .OnDelete(DeleteBehavior.Cascade);
        
        base.OnModelCreating(modelBuilder);
    }
}