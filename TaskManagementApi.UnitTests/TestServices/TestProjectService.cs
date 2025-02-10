using FakeItEasy;
using TaskManagementApi.Data.Entities;
using TaskManagementApi.Data.Repositories;
using TaskManagementApi.Models.Enums;
using TaskManagementApi.Models.Project;
using TaskManagementApi.Services.ProjectHandling;
using Task = System.Threading.Tasks.Task;
using TimeProvider = TaskManagementApi.Services.TimeProvider;

namespace TaskManagementApi.UnitTests.TestServices;

[TestFixture]
public class TestProjectService
{
    [Test]
    public async Task CreateProject_ValidInput_ReturnsValidProjectDto()
    {
        // Arrange
        const string projectTitle = "A project title";
        const string projectDescription = "A project description";
        var projectCreatedDate = new DateTime(1970, 1, 1);
        var projectDueDate = new DateTime(1970, 2, 1);
        
        var createProjectDto = new CreateProjectDto
        {
            Title = projectTitle,
            Description = projectDescription,
            DueDate = projectDueDate,
            OwnerId = 0
        };
        
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);
        
        TimeProvider.SetCustomNow(projectCreatedDate);
        
        // Act
        ProjectDto actualProject = await projectService.CreateProject(createProjectDto);
        
        TimeProvider.Reset();

        // Assert
        var expectedProject = new ProjectDto
        {
            Id = 0,
            Name = projectTitle,
            Description = projectDescription,
            CreatedDate = projectCreatedDate,
            DueDate = projectDueDate,
            Status = ProjectStatus.Active
        };
        Assert.Multiple(() =>
        {
            Assert.That(actualProject.Id, Is.EqualTo(expectedProject.Id));
            Assert.That(actualProject.Name, Is.EqualTo(expectedProject.Name));
            Assert.That(actualProject.Description, Is.EqualTo(expectedProject.Description));
            Assert.That(actualProject.CreatedDate, Is.EqualTo(expectedProject.CreatedDate));
            Assert.That(actualProject.DueDate, Is.EqualTo(expectedProject.DueDate));
            Assert.That(actualProject.Status, Is.EqualTo(expectedProject.Status));
        });
    }  
    
    [Test]
    public async Task DeleteProject_ProjectWithIdExists_CallsRepository()
    {
        var projectRepositoryMock = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepositoryMock);

        await projectService.DeleteProject(0);

        A.CallTo(() => projectRepositoryMock.DeleteAsync(A<Project>.Ignored)).MustHaveHappened();
    }
    
    [Test]
    public void DeleteProject_ProjectWithIdNotExists_ThrowsKeyNotFoundException()
    {
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);

        Project? valueToReturn = null;
        A.CallTo(() => projectRepository.GetByIdAsync(An<int>.Ignored)).Returns(valueToReturn);

        Assert.ThrowsAsync<KeyNotFoundException>(() => projectService.DeleteProject(0));
    }
}