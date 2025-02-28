namespace TaskManagementApi.Services.TimeProvider;

public interface ITimeProvider
{
    public DateTime Now { get; }
    public DateTime UtcNow { get; }
}