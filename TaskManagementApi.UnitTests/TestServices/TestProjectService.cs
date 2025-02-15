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
    public async Task GetAllProjects_ProjectsExist_ReturnsCollectionOfProjects()
    {
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);
        
        A.CallTo(() => projectRepository.GetAllAsync()).Returns([new Project(), new Project()]);
        
        List<ProjectDto> projects = await projectService.GetAllProjects();
        
        Assert.Multiple(() =>
        {
            Assert.That(projects, Is.Not.Null);
            Assert.That(projects, Is.Not.Empty);
        });
    }

    [Test]
    public async Task GetAllProjects_NoProjects_ReturnsEmptyCollection()
    {
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);
        
        A.CallTo(() => projectRepository.GetAllAsync()).Returns([]);
        
        List<ProjectDto> projects = await projectService.GetAllProjects();
        
        Assert.Multiple(() =>
        {
            Assert.That(projects, Is.Not.Null);
            Assert.That(projects, Is.Empty);
        });
    }
    
    [Test]
    public async Task GetProject_ProjectWithIdExists_ReturnsProject()
    {
        // Arrange
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);

        A.CallTo(() => projectRepository.GetByIdAsync(An<int>.Ignored)).Returns(new Project
        {
            Id = 0,
            Name = "A project name",
            Description = "A project description",
            CreatedDate = new DateTime(1970, 1, 1),
            DueDate = new DateTime(1970, 2, 1),
            Status = Data.Entities.Enums.ProjectStatus.Active
        });
        
        // Act
        ProjectDto actualProjectDto = await projectService.GetProject(0);

        // Assert
        var expectedProjectDto = new ProjectDto
        {
            Id = 0,
            Name = "A project name",
            Description = "A project description",
            CreatedDate = new DateTime(1970, 1, 1),
            DueDate = new DateTime(1970, 2, 1),
            Status = ProjectStatus.Active
        };
        Assert.Multiple(() =>
        {
            Assert.That(actualProjectDto.Id, Is.EqualTo(expectedProjectDto.Id));
            Assert.That(actualProjectDto.Name, Is.EqualTo(expectedProjectDto.Name));
            Assert.That(actualProjectDto.Description, Is.EqualTo(expectedProjectDto.Description));
            Assert.That(actualProjectDto.CreatedDate, Is.EqualTo(expectedProjectDto.CreatedDate));
            Assert.That(actualProjectDto.DueDate, Is.EqualTo(expectedProjectDto.DueDate));
            Assert.That(actualProjectDto.Status, Is.EqualTo(expectedProjectDto.Status));
        });
    }

    [Test]
    public void GetProject_ProjectWithDoesNotExist_ThrowsKeyNotFoundException()
    {
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);
        
        Project? valueToReturn = null;
        A.CallTo(() => projectRepository.GetByIdAsync(An<int>.Ignored)).Returns(valueToReturn);
        
        Assert.ThrowsAsync<KeyNotFoundException>(() => projectService.GetProject(0));
    }

    [Test]
    public void CreateProject_DueDateBeforeCreatedDate_ThrowsArgumentException()
    {
        // Arrange
        var projectCreatedDate = new DateTime(1970, 2, 1);
        var projectDueDate = new DateTime(1970, 1, 1);
        
        var createProjectDto = new CreateProjectDto
        {
            Title =  "A project title",
            Description = "A project description",
            DueDate = projectDueDate,
            OwnerId = 0
        };
        
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);
        
        TimeProvider.SetCustomNow(projectCreatedDate);

        try
        {
            Assert.ThrowsAsync<ArgumentException>(() => projectService.CreateProject(createProjectDto));
        }
        finally
        {
            TimeProvider.Reset();
        }
    }
    
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
    public void UpdateProject_ProjectWithIdDoesNotExist_ThrowsKeyNotFoundException()
    {
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);
        var updateProjectDto = new UpdateProjectDto();
        
        Project? valueToReturn = null;
        A.CallTo(() => projectRepository.GetByIdAsync(An<int>.Ignored)).Returns(valueToReturn);
        
        Assert.ThrowsAsync<KeyNotFoundException>(() => projectService.UpdateProject(0, updateProjectDto));
    }
    
    [Test]
    public void UpdateProject_NameSpecifiedButAnEmptyString_ThrowsArgumentException()
    {
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);
        var updateProjectDto = new UpdateProjectDto
        {
            Name = string.Empty
        };
        
        Assert.ThrowsAsync<ArgumentException>(() => projectService.UpdateProject(0, updateProjectDto));
    }
    
    [Test]
    public void UpdateProject_DueDateBeforeCreatedDate_ThrowsArgumentException()
    {
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);
        
        A.CallTo(() => projectRepository.GetByIdAsync(An<int>.Ignored)).Returns(new Project
        {
            CreatedDate = new DateTime(1970, 1, 1)
        });
        
        var updateProjectDto = new UpdateProjectDto
        {
            DueDate = DateTime.MinValue
        };
        
        Assert.ThrowsAsync<ArgumentException>(() => projectService.UpdateProject(0, updateProjectDto));
    }
    
    [Test]
    public async Task UpdateProject_EmptyDto_CallsUpdateOnRepository()
    {
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);
        var updateProjectDto = new UpdateProjectDto();
        
        await projectService.UpdateProject(0, updateProjectDto);
        
        A.CallTo(() => projectRepository.UpdateAsync(A<Project>.Ignored)).MustHaveHappened();
    }
    
    [Test]
    public async Task UpdateProject_ValidNonEmptyDto_CallsUpdateOnRepository()
    {
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);
        var updateProjectDto = new UpdateProjectDto
        {
            Name = "A project name",
            Description = "A project description",
            DueDate = DateTime.MaxValue,
            ProjectStatus = ProjectStatus.OnHold
        };
        
        await projectService.UpdateProject(0, updateProjectDto);
        
        A.CallTo(() => projectRepository.UpdateAsync(A<Project>.Ignored)).MustHaveHappened();
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
    public void DeleteProject_ProjectWithIdDoesNotExist_ThrowsKeyNotFoundException()
    {
        var projectRepository = A.Fake<IProjectRepository>();
        var projectService = new ProjectService(projectRepository);

        Project? valueToReturn = null;
        A.CallTo(() => projectRepository.GetByIdAsync(An<int>.Ignored)).Returns(valueToReturn);

        Assert.ThrowsAsync<KeyNotFoundException>(() => projectService.DeleteProject(0));
    }
}