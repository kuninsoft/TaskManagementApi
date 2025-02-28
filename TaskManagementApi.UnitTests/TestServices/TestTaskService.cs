using FakeItEasy;
using TaskManagementApi.Data.Entities;
using TaskManagementApi.Data.Entities.Enums;
using TaskManagementApi.Models.Project;
using TaskManagementApi.Models.Tasks;
using TaskManagementApi.Services.TaskHandling;
using TaskManagementApi.UnitTests.Factories;
using Task = System.Threading.Tasks.Task;
using TaskEntity = TaskManagementApi.Data.Entities.Task;
using TaskStatus = TaskManagementApi.Data.Entities.Enums.TaskStatus;

namespace TaskManagementApi.UnitTests.TestServices;

[TestFixture]
public class TestTaskService
{
    [Test]
    public async Task GetAllTasks_TasksExist_ReturnsNonEmptyCollection()
    {
        var factory = new TestTaskServiceFactory();

        A.CallTo(() => factory.TaskRepository.GetAllAsync()).Returns([
            new TaskEntity {Project = new Project()}, new TaskEntity {Project = new Project()}
        ]);
        
        TaskService taskService = factory.Create();
        
        List<TaskDto> tasks = await taskService.GetAllTasks();
        
        Assert.That(tasks, Is.Not.Empty);
    }

    [Test]
    public async Task GetAllTasks_NoTasks_ReturnsEmptyCollection()
    {
        var factory = new TestTaskServiceFactory();
        
        A.CallTo(() => factory.TaskRepository.GetAllAsync()).Returns([]);
        
        TaskService taskService = factory.Create();
        
        List<TaskDto> tasks = await taskService.GetAllTasks();
        
        Assert.That(tasks, Is.Empty);
    }

    [Test]
    public async Task GetTask_TaskWithIdExists_ReturnsTask()
    {
        // Arrange
        var factory = new TestTaskServiceFactory();

        A.CallTo(() => factory.TaskRepository.GetByIdAsync(An<int>.Ignored)).Returns(new TaskEntity
        {
            Id = 0,
            Title = "A task title",
            Description = "A task description",
            Status = TaskStatus.InProgress,
            Priority = TaskPriority.Medium,
            CreatedDate = new DateTime(1970, 1, 1),
            Project = new Project()
        });

        TaskService taskService = factory.Create();
            
        // Act
        TaskDto actualTask = await taskService.GetTask(0);

        // Assert
        var expectedTask = new TaskDto
        {
            Id = 0,
            Title = "A task title",
            Description = "A task description",
            Status = Models.Enums.TaskStatus.InProgress,
            Priority = Models.Enums.TaskPriority.Medium,
            CreatedDate = new DateTime(1970, 1, 1),
            Project = new ProjectSummaryDto()
        };
        Assert.Multiple(() =>
        {
            Assert.That(actualTask.Id, Is.EqualTo(expectedTask.Id));
            Assert.That(actualTask.Title, Is.EqualTo(expectedTask.Title));
            Assert.That(actualTask.Description, Is.EqualTo(expectedTask.Description));
            Assert.That(actualTask.Status, Is.EqualTo(expectedTask.Status));
            Assert.That(actualTask.Priority, Is.EqualTo(expectedTask.Priority));
            Assert.That(actualTask.CreatedDate, Is.EqualTo(expectedTask.CreatedDate));
        });
    }

    [Test]
    public void GetTask_TaskWithIdDoesNotExist_ThrowsKeyNotFoundException()
    {
        var factory = new TestTaskServiceFactory();
        TaskEntity? valueToReturn = null;
        A.CallTo(() => factory.TaskRepository.GetByIdAsync(An<int>.Ignored)).Returns(valueToReturn);
        TaskService taskService = factory.Create();
        
        Assert.ThrowsAsync<KeyNotFoundException>(() => taskService.GetTask(0));
    }

    [Test]
    public async Task CreateTask_ValidInput_ReturnsValidTask()
    {
        // Arrange
        var factory = new TestTaskServiceFactory();
        
        A.CallTo(() => factory.TaskRepository.CreateAsync(An<TaskEntity>.Ignored)).Returns(new TaskEntity
        {
            Id = 0,
            Title = "A task title",
            Description = "A task description",
            Priority = TaskPriority.Medium,
            CreatedDate = new DateTime(1970, 1, 1),
            Project = new Project { Id = 1, Name = "Project 1", Status = ProjectStatus.Active }
        });

        TaskService taskService = factory.Create();

        // Act
        TaskDto actualTask = await taskService.CreateTask(new CreateTaskDto
        {
            Title = "A task title",
            Description = "A task description",
            ProjectId = 1,
            ReporterUserId = 1
        }); 
        
        // Assert 
        var expectedTask = new TaskDto
        {
            Id = 0,
            Title = "A task title",
            Description = "A task description",
            Priority = Models.Enums.TaskPriority.Medium,
            CreatedDate = new DateTime(1970, 1, 1),
            Project = new ProjectSummaryDto()
        };
        Assert.Multiple(() =>
        {
            Assert.That(actualTask.Id, Is.EqualTo(expectedTask.Id));
            Assert.That(actualTask.Title, Is.EqualTo(expectedTask.Title));
            Assert.That(actualTask.Description, Is.EqualTo(expectedTask.Description));
            Assert.That(actualTask.Priority, Is.EqualTo(expectedTask.Priority));
            Assert.That(actualTask.CreatedDate, Is.EqualTo(expectedTask.CreatedDate));
        });
    }

    [Test]
    public void CreateTask_DueDateBeforeCreatedDate_ThrowsArgumentException()
    {
        var taskDueDate = new DateTime(1969, 1, 1);
        var createTaskDto = new CreateTaskDto
        {
            Title = "A task title",
            Description = "A task description",
            ProjectId = 1,
            ReporterUserId = 1,
            DueDate = taskDueDate
        };

        TaskService taskService = new TestTaskServiceFactory().Create();
        
        Assert.ThrowsAsync<ArgumentException>(() => taskService.CreateTask(createTaskDto));
    }

    [Test]
    public void UpdateTask_TaskWithIdDoesNotExist_ThrowsKeyNotFoundException()
    {
        var factory = new TestTaskServiceFactory();
        TaskEntity? valueToReturn = null;
        A.CallTo(() => factory.TaskRepository.GetByIdAsync(An<int>.Ignored)).Returns(valueToReturn);
        TaskService taskService = factory.Create();
        
        Assert.ThrowsAsync<KeyNotFoundException>(() => taskService.UpdateTask(0, new UpdateTaskDto()));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("\n")]
    public void UpdateTask_TitleSpecifiedButAnEmptyString_ThrowsArgumentException(string title)
    {
        var updateTaskDto = new UpdateTaskDto
        {
            Title = title
        };
        
        TaskService taskService = new TestTaskServiceFactory().Create();
        
        Assert.ThrowsAsync<ArgumentException>(() => taskService.UpdateTask(0, updateTaskDto));
    }

    [Test]
    public void UpdateTask_DueDateBeforeCreatedDate_ThrowsArgumentException()
    {
        var updateTaskDto = new UpdateTaskDto
        {
            DueDate = new DateTime(1969, 1, 1)
        };

        var factory = new TestTaskServiceFactory();

        A.CallTo(() => factory.TaskRepository.GetByIdAsync(An<int>.Ignored)).Returns(new TaskEntity
        {
            CreatedDate = new DateTime(1970, 1, 1)
        });
        
        TaskService taskService = factory.Create();
        
        Assert.ThrowsAsync<ArgumentException>(() => taskService.UpdateTask(0, updateTaskDto));
    }

    [Test]
    public async Task UpdateTask_EmptyDto_CallsUpdateOnRepository()
    {
        var updateTaskDto = new UpdateTaskDto();
        var factory = new TestTaskServiceFactory();
        TaskService taskService = factory.Create();
        
        await taskService.UpdateTask(0, updateTaskDto);
        
        A.CallTo(() => factory.TaskRepository.UpdateAsync(A<TaskEntity>.Ignored)).MustHaveHappened();
    }
    
    [Test]
    public async Task UpdateTask_ValidNonEmptyDto_CallsUpdateOnRepository()
    {
        var updateTaskDto = new UpdateTaskDto
        {
            Title = "A task title",
            Description = "A task description",
            AssignedUserId = 1,
            DueDate = new DateTime(2020, 1, 1),
            IsFlagged = false,
            Priority = Models.Enums.TaskPriority.High,
            Status = Models.Enums.TaskStatus.InProgress,
            ReporterUserId = 2
        };

        var factory = new TestTaskServiceFactory();
        TaskService taskService = factory.Create();
        
        await taskService.UpdateTask(0, updateTaskDto);
        
        A.CallTo(() => factory.TaskRepository.UpdateAsync(A<TaskEntity>.Ignored)).MustHaveHappened();
    }

    [Test]
    public async Task DeleteTask_ProjectWithIdExists_CallsRepository()
    {
        var factory = new TestTaskServiceFactory();
        TaskService taskService = factory.Create();
        
        await taskService.DeleteTask(0);
        
        A.CallTo(() => factory.TaskRepository.DeleteAsync(A<TaskEntity>.Ignored)).MustHaveHappened();
    }
    
    [Test]
    public void DeleteTask_ProjectWithIdDoesNotExist_ThrowsKeyNotFoundException()
    {
        var factory = new TestTaskServiceFactory();
        
        TaskEntity? valueToReturn = null;
        A.CallTo(() => factory.TaskRepository.GetByIdAsync(An<int>.Ignored)).Returns(valueToReturn);
        
        TaskService taskService = factory.Create();
        
        Assert.ThrowsAsync<KeyNotFoundException>(() => taskService.DeleteTask(0));
    }
}