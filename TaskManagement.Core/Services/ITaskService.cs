using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Requests;

namespace TaskManagement.Core.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId);
    Task<TaskItem> GetTaskByIdAsync(int taskId);
    Task<TaskItem> AddTaskAsync(int projectId, TaskRequest task);
    Task<bool> UpdateTaskAsync(int taskId, TaskRequestUpdate task);
    Task<bool> RemoveTaskAsync(int taskId);
    Task<bool> AddCommentAsync(int taskId, CommentRequest comment);
    Task<bool> AddTaskHistoryAsync(int taskId, string changeDescription, string modifiedBy);
    Task<double> GetAvergeCompletedTasks();
}