using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManagementApi.Data.Entities;

namespace TaskManagementApi.Data;

public class AppDbContext : DbContext
{
    private readonly DatabaseOptions _options;
    
    private readonly string _dbPath;
    
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Entities.Task> Tasks { get; set; }

    public AppDbContext(IOptions<DatabaseOptions> options)
    {
        _options = options.Value;
        
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _dbPath = Path.Join(path, _options.FileName); 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(string.Format(_options.ConnectionString, _dbPath));

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