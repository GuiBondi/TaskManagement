using TaskManagement.Core.Models;

namespace TaskManagement.Core.Repository;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId);
    Task<TaskItem> GetTaskByIdAsync(int taskId);
    Task<TaskItem> AddTaskAsync(TaskItem task);
    Task<bool> UpdateTaskAsync(TaskItem task);
    Task<bool> RemoveTaskAsync(int taskId);
    Task<int> GetTaskCountByProjectIdAsync(int projectId);
    Task<double> GetAvergeCompletedTasks();
}