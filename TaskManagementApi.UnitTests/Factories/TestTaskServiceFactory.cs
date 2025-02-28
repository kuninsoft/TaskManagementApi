using FakeItEasy;
using TaskManagementApi.Data.Repositories;
using TaskManagementApi.Services.TaskHandling;
using TaskManagementApi.Services.TimeProvider;

namespace TaskManagementApi.UnitTests.Factories;

public class TestTaskServiceFactory
{
    public ITaskRepository TaskRepository { get; } = A.Fake<ITaskRepository>();
    public ITimeProvider TimeProvider { get; } = A.Fake<ITimeProvider>();

    public TaskService Create()
    {
        A.CallTo(() => TimeProvider.Now).Returns(new DateTime(1970, 1, 1));
        A.CallTo(() => TimeProvider.UtcNow).Returns(new DateTime(1970, 1, 1));
        
        return new TaskService(TaskRepository, TimeProvider);
    }
}