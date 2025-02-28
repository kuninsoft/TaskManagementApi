namespace TaskManagementApi.Services.TimeProvider;

public class TimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}