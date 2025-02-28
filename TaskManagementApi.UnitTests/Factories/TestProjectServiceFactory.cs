using FakeItEasy;
using TaskManagementApi.Data.Repositories;
using TaskManagementApi.Services.ProjectHandling;
using TaskManagementApi.Services.TimeProvider;

namespace TaskManagementApi.UnitTests.Factories;

public class TestProjectServiceFactory
{
    public IProjectRepository ProjectRepository { get; } = A.Fake<IProjectRepository>();
    public ITimeProvider TimeProvider { get; } = A.Fake<ITimeProvider>();

    public ProjectService Create()
    {
        A.CallTo(() => TimeProvider.Now).Returns(new DateTime(1970, 1, 1));
        A.CallTo(() => TimeProvider.UtcNow).Returns(new DateTime(1970, 1, 1));
        
        return new ProjectService(ProjectRepository, TimeProvider);
    }
}