using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Data;
using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Enums;

namespace TaskManagement.Core.Repository.Impl;

public class TaskRepository(ApplicationDbContext context) : ITaskRepository
{
    public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId)
    {
        return await context.TaskItems
            .Where(t => t.ProjectId == projectId)
            .Include(t => t.Comments)
            .Include(t => t.Histories)
            .ToListAsync();
    }

    public async Task<TaskItem> GetTaskByIdAsync(int taskId)
    {
        return await context.TaskItems
            .Include(t => t.Comments)
            .Include(t => t.Histories)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public async Task<TaskItem> AddTaskAsync(TaskItem task)
    {
        context.TaskItems.Add(task);
        await context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateTaskAsync(TaskItem task)
    {
        context.TaskItems.Update(task);
        return (await context.SaveChangesAsync()) > 0;
    }

    public async Task<bool> RemoveTaskAsync(int taskId)
    {
        var task = await GetTaskByIdAsync(taskId);
        if (task == null)
            return false;

        context.TaskItems.Remove(task);
        return (await context.SaveChangesAsync()) > 0;
    }

    public async Task<int> GetTaskCountByProjectIdAsync(int projectId)
    {
        return await context.TaskItems.CountAsync(t => t.ProjectId == projectId);
    }

    public async Task<double> GetAvergeCompletedTasks()
    {
        var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
        var tasksCompleted = await context.TaskItems
            .Where(t => t.Status == (int)TaskItemStatus.Concluida && t.DueDate >= thirtyDaysAgo)
            .GroupBy(t => t.ProjectId)
            .Select(g => new { UserId = g.Key, Count = g.Count() })
            .ToListAsync();

        return tasksCompleted.Average(t => t.Count);
    }
}