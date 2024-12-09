using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace TaskManagementApi.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var dbOptions = new DatabaseOptions()
        {
            ConnectionString = "Data Source={0}",
            FileName = "api.db"
        };
        
        return new AppDbContext(new OptionsWrapper<DatabaseOptions>(dbOptions));
    }
}