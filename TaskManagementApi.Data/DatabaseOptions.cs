namespace TaskManagementApi.Data;

public class DatabaseOptions
{
    public const string DbConfiguration = "DbConfiguration";

    public string ConnectionString { get; init; } = null!;
    public string FileName { get; init; } = null!;
}