using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Data;
using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Enums;
using TaskManagement.Core.Repository.Impl;

namespace TaskManagement.test.Repositories;

public class TaskRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly TaskRepository _taskRepository;

    public TaskRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TaskRepositoryTests")
            .Options;

        _context = new ApplicationDbContext(options);
        _taskRepository = new TaskRepository(_context);
    }

    [Fact]
    public async Task AddTaskAsync_ShouldAddTask()
    {
        var task = new TaskItem
        {
            Title = "Nova Tarefa",
            Priority = (int)Priority.Alta,
            Status = (int)TaskItemStatus.Pendente,
            DueDate = DateTime.UtcNow.AddDays(5),
            ProjectId = 1,
            Description = "Teste",
        };
        
        var result = await _taskRepository.AddTaskAsync(task);
        Assert.NotNull(result);
        Assert.Equal("Nova Tarefa", result.Title);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetTaskByIdAsync_ShouldReturnTask_WhenTaskExists()
    {
        var task = new TaskItem
        {
            Title = "Tarefa Existente",
            Description = "Teste",
            Priority = (int)Priority.Media,
            Status = (int)TaskItemStatus.EmAndamento,
            DueDate = DateTime.UtcNow.AddDays(3),
            ProjectId = 1
        };
        await _context.TaskItems.AddAsync(task);
        await _context.SaveChangesAsync();
        
        var result = await _taskRepository.GetTaskByIdAsync(task.Id);
        
        Assert.NotNull(result);
        Assert.Equal("Tarefa Existente", result.Title);
    }

    [Fact]
    public async Task RemoveTaskAsync_ShouldRemoveTask()
    {
        var task = new TaskItem
        {
            Title = "Tarefa a Remover",
            Priority = (int)Priority.Baixa,
            Status = (int)TaskItemStatus.Pendente,
            DueDate = DateTime.UtcNow.AddDays(2),
            ProjectId = 1,
            Description = "Teste",
        };
        await _context.TaskItems.AddAsync(task);
        await _context.SaveChangesAsync();
        var result = await _taskRepository.RemoveTaskAsync(task.Id);
        Assert.True(result);
        var taskInDb = await _context.TaskItems.FindAsync(task.Id);
        Assert.Null(taskInDb);
    }
}