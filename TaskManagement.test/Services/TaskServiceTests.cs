using Moq;
using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Enums;
using TaskManagement.Core.Models.Requests;
using TaskManagement.Core.Repository;
using TaskManagement.Core.Services;
using TaskManagement.Core.Services.Impl;

namespace TaskManagement.test.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly ITaskService _taskService;

        public TaskServiceTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _taskService = new TaskService(_taskRepositoryMock.Object, _projectRepositoryMock.Object);
        }

        [Fact]
        public async Task AddTaskAsync_ShouldThrowException_WhenProjectNotFound()
        {
            _projectRepositoryMock.Setup(repo => repo.GetProjectByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Project)null);
            var newTask = new TaskRequest()
            {
                Title = "Nova Tarefa",
                Priority = Priority.Alta
            };
            await Assert.ThrowsAsync<Exception>(() => _taskService.AddTaskAsync(1, newTask));
        }

        [Fact]
        public async Task AddTaskAsync_ShouldThrowException_WhenTaskLimitReached()
        {
         
            _projectRepositoryMock.Setup(repo => repo.GetProjectByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Project { Id = 1, Name = "Projeto 1" });

            _taskRepositoryMock.Setup(repo => repo.GetTaskCountByProjectIdAsync(It.IsAny<int>()))
                .ReturnsAsync(20);

            var newTask = new TaskRequest()
            {
                Title = "Nova Tarefa",
                Priority = Priority.Alta
            };
            await Assert.ThrowsAsync<Exception>(() => _taskService.AddTaskAsync(1, newTask));
        }

        [Fact]
        public async Task UpdateTaskAsync_ShouldNotAllowPriorityChange()
        {
            var existingTask = new TaskItem
            {
                Id = 1,
                Title = "Tarefa Existente",
                Priority =(int)Priority.Media
            };

            _taskRepositoryMock.Setup(repo => repo.GetTaskByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(existingTask);

            var updatedTask = new TaskRequestUpdate
            {
             
                Description = "Tarefa Existente",
                Status = TaskItemStatus.EmAndamento
            };

            _taskRepositoryMock.Setup(repo => repo.UpdateTaskAsync(It.IsAny<TaskItem>()))
                .ReturnsAsync(true);
            var result = await _taskService.UpdateTaskAsync(1, updatedTask);
            Assert.True(result);
            Assert.Equal((int)Priority.Media, existingTask.Priority);
        }

        [Fact]
        public async Task AddCommentAsync_ShouldAddCommentAndRegisterHistory()
        {
            var task = new TaskItem
            {
                Id = 1,
                Title = "Tarefa"
            };

            _taskRepositoryMock.Setup(repo => repo.GetTaskByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(task);

            _taskRepositoryMock.Setup(repo => repo.UpdateTaskAsync(It.IsAny<TaskItem>()))
                .ReturnsAsync(true);

            var comment = new CommentRequest()
            {
                Content = "Novo Comentário",
            };
            var result = await _taskService.AddCommentAsync(1, comment);
            Assert.True(result);
            _taskRepositoryMock.Verify(repo => repo.UpdateTaskAsync(It.IsAny<TaskItem>()), Times.Once);
            Assert.Single(task.Comments);
            Assert.Single(task.Histories);
        }
    }
}
