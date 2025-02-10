namespace TaskManagementApi.Services;

public static class TimeProvider
{
    private static bool IsConfiguredCustom { get; set; }
    private static DateTime CustomNow { get; set; }
    
    public static DateTime Now => IsConfiguredCustom ? CustomNow : DateTime.Now;
    public static DateTime UtcNow => IsConfiguredCustom ? CustomNow : DateTime.UtcNow;

    public static void SetCustomNow(DateTime customNow)
    {
        IsConfiguredCustom = true;
        CustomNow = customNow;
    }

    public static void Reset()
    {
        IsConfiguredCustom = false;
        CustomNow = default;
    }
}