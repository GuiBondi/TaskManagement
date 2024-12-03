using Moq;
using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Requests;
using TaskManagement.Core.Repository;
using TaskManagement.Core.Services;
using TaskManagement.Core.Services.Impl;
namespace TaskManagement.test.Services;

    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly IProjectService _projectService;

        public ProjectServiceTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _projectService = new ProjectService(_projectRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllProjectsAsync_ShouldReturnListOfProjects()
        {
            var projects = new List<Project>
            {
                new Project { Id = 1, Name = "Projeto 1" },
                new Project { Id = 2, Name = "Projeto 2" }
            };
            _projectRepositoryMock.Setup(repo => repo.GetAllProjectsAsync())
                .ReturnsAsync(projects);
       
            var result = await _projectService.GetAllProjectsAsync();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task RemoveProjectAsync_ShouldReturnFalse_WhenProjectNotFound()
        {
            _projectRepositoryMock.Setup(repo => repo.RemoveProjectAsync(It.IsAny<int>()))
                .ReturnsAsync(false);
            
            var result = await _projectService.RemoveProjectAsync(1);
            Assert.False(result);
        }

        [Fact]
        public async Task RemoveProjectAsync_ShouldReturnFalse_WhenProjectHasPendingTasks()
        {
            _projectRepositoryMock.Setup(repo => repo.HasPendingTasksAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            var hasPendingTasks = await _projectService.HasPendingTasksAsync(1);
            Assert.True(hasPendingTasks);
        }

        [Fact]
        public async Task AddProjectAsync_ShouldAddProjectSuccessfully()
        {
         
            var newProject = new CreateProjectRequest() { Name = "Novo Projeto" };
            _projectRepositoryMock.Setup(repo => repo.AddProjectAsync(It.IsAny<Project>()))
                .ReturnsAsync(new Project { Id = 1, Name = "Novo Projeto" });

       
            var result = await _projectService.AddProjectAsync(newProject);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Novo Projeto", result.Name);
        }
    }

