using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Data;
using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Enums;
using TaskManagement.Core.Repository;

namespace TaskManagement.test.Repositories
{
    public class ProjectRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ProjectRepository _projectRepository;

        public ProjectRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectRepositoryTests")
                .Options;

            _context = new ApplicationDbContext(options);
            _projectRepository = new ProjectRepository(_context);
        }

        [Fact]
        public async Task AddProjectAsync_ShouldAddProject()
        {
            var project = new Project { Name = "Projeto Teste" };
            var result = await _projectRepository.AddProjectAsync(project);
            Assert.NotNull(result);
            Assert.Equal("Projeto Teste", result.Name);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task HasPendingTasksAsync_ShouldReturnTrue_WhenThereArePendingTasks()
        {
            var project = new Project { Name = "Projeto" };
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            var task = new TaskItem
            {
                ProjectId = project.Id,
                Title = "Tarefa Pendente",
                Status = (int)TaskItemStatus.Criada,
                Priority = (int)Priority.Alta
            };
            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();
            var result = await _projectRepository.HasPendingTasksAsync(project.Id);
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveProjectAsync_ShouldRemoveProject_WhenNoPendingTasks()
        {
           
            var project = new Project { Name = "Projeto" };
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            var result = await _projectRepository.RemoveProjectAsync(project.Id);
            Assert.True(result);
            var projectInDb = await _context.Projects.FindAsync(project.Id);
            Assert.Null(projectInDb);
        }
    }
}
