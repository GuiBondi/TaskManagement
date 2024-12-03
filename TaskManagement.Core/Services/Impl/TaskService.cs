// using TaskManagement.Core.Models;
// using TaskManagement.Core.Models.Requests;
// using TaskManagement.Core.Repository;
//
// namespace TaskManagement.Core.Services.Impl;
//
// public class TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository)
//     : ITaskService
// {
//     public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId)
//     {
//         return await taskRepository.GetTasksByProjectIdAsync(projectId);
//     }
//
//     public async Task<TaskItem> GetTaskByIdAsync(int taskId)
//     {
//         return await taskRepository.GetTaskByIdAsync(taskId);
//     }
//
//     public async Task<TaskItem> AddTaskAsync(int projectId, TaskRequest request)
//     {
//         var task = new TaskItem()
//         {
//             ProjectId = projectId,
//             Title = request.Title,
//             Description = request.Description,
//             DueDate = request.DueDate,
//             Status = (int)request.Status,
//             Priority = (int)request.Priority
//         };
//         await ProcessTaskRequest(projectId, request);
//
//         var addedTask = await taskRepository.AddTaskAsync(task);
//         await AddTaskHistoryAsync(addedTask.Id, "Tarefa criada.", "Usuário X");
//         return addedTask;
//     }
//
//     private async Task ProcessTaskRequest(int projectId, TaskRequest task)
//     {
//         var project = await projectRepository.GetProjectByIdAsync(projectId);
//         if (project == null)
//             throw new Exception("Projeto não encontrado.");
//         var taskCount = await taskRepository.GetTaskCountByProjectIdAsync(projectId);
//         if (taskCount >= 20)
//             throw new Exception("O projeto já atingiu o limite máximo de 20 tarefas.");
//
//         if (task.Priority is 0)
//             throw new Exception("Uma prioridade deve ser informada.");
//     }
//
//     public async Task<bool> UpdateTaskAsync(int taskId, TaskRequestUpdate updatedTask)
//     {
//         var existingTask = await ProcessUpdateTask(taskId, updatedTask);
//
//         var result = await taskRepository.UpdateTaskAsync(existingTask);
//
//         await AddTaskHistoryAsync(taskId, "Tarefa atualizada.", "Usuário X");
//
//         return result;
//     }
//
//     private async Task<TaskItem?> ProcessUpdateTask(int taskId, TaskRequestUpdate updatedTask)
//     {
//         var existingTask = await taskRepository.GetTaskByIdAsync(taskId);
//         if (existingTask == null)
//             throw new Exception("Tarefa não encontrada.");
//         existingTask.Description = updatedTask.Description;
//         existingTask.Status = (int)updatedTask.Status;
//         return existingTask;
//     }
//
//     public async Task<bool> RemoveTaskAsync(int taskId)
//     {
//         var result = await taskRepository.RemoveTaskAsync(taskId);
//         return result;
//     }
//
//     public async Task<bool> AddCommentAsync(int taskId, CommentRequest request)
//     {
//         var (task, comment) = await ProcessAddComents(taskId, request);
//         var result = await taskRepository.UpdateTaskAsync(task);
//         await AddTaskHistoryAsync(taskId, "Comentário adicionado.", comment.CommentedBy);
//         return result;
//     }
//
//     private async Task<(TaskItem?, Comment)> ProcessAddComents(int taskId, CommentRequest request)
//     {
//         var task = await taskRepository.GetTaskByIdAsync(taskId);
//         if (task == null)
//             throw new Exception("Tarefa não encontrada.");
//         var comment = new Comment()
//         {
//             TaskId = taskId,
//             Content = request.Content,
//             CommentedAt = DateTime.UtcNow,
//             CommentedBy = "Usuario teste"
//         };
//         task.Comments.Add(comment);
//         return (task, comment);
//     }
//
//     public async Task<bool> AddTaskHistoryAsync(int taskId, string changeDescription, string modifiedBy)
//     {
//         var task = await taskRepository.GetTaskByIdAsync(taskId);
//         if (task == null)
//             return false;
//
//         var history = new TaskHistory
//         {
//             TaskId = taskId,
//             ChangeDescription = changeDescription,
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = modifiedBy
//         };
//
//         task.Histories.Add(history);
//         return await taskRepository.UpdateTaskAsync(task);
//     }
//
//     private bool IsValidPriority(string priority)
//     {
//         var validPriorities = new[] { "baixa", "média", "alta" };
//         return Array.Exists(validPriorities, p => p.Equals(priority, StringComparison.OrdinalIgnoreCase));
//     }
//
//     public async Task<double> GetAvergeCompletedTasks() => await taskRepository.GetAvergeCompletedTasks();
// }
using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Requests;
using TaskManagement.Core.Repository;

namespace TaskManagement.Core.Services.Impl;

public class TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository)
    : ITaskService
{
    public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId)
    {
        return await taskRepository.GetTasksByProjectIdAsync(projectId);
    }

    public async Task<TaskItem> GetTaskByIdAsync(int taskId)
    {
        return await taskRepository.GetTaskByIdAsync(taskId);
    }

    public async Task<TaskItem> AddTaskAsync(int projectId, TaskRequest request)
    {
        await ProcessTaskRequest(projectId, request);

        var task = new TaskItem
        {
            ProjectId = projectId,
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            Status = (int)request.Status,
            Priority = (int)request.Priority
        };

        var addedTask = await taskRepository.AddTaskAsync(task);

        await AddTaskHistoryAsync(addedTask.Id, "Tarefa criada.", "Usuário X");

        return addedTask;
    }

    private async Task ProcessTaskRequest(int projectId, TaskRequest task)
    {
        var project = await projectRepository.GetProjectByIdAsync(projectId);
        if (project == null)
            throw new Exception("Projeto não encontrado.");

        var taskCount = await taskRepository.GetTaskCountByProjectIdAsync(projectId);
        if (taskCount >= 20)
            throw new Exception("O projeto já atingiu o limite máximo de 20 tarefas.");

        if (task.Priority is 0)
            throw new Exception("Uma prioridade deve ser informada.");
    }

    public async Task<bool> UpdateTaskAsync(int taskId, TaskRequestUpdate updatedTask)
    {
        var task = await taskRepository.GetTaskByIdAsync(taskId);
        if (task == null)
            throw new Exception("Tarefa não encontrada.");

        task.Description = updatedTask.Description;
        task.Status = (int)updatedTask.Status;

        var result = await taskRepository.UpdateTaskAsync(task);

        await AddTaskHistoryAsync(taskId, "Tarefa atualizada.", "Usuário X");

        return result;
    }

    public async Task<bool> RemoveTaskAsync(int taskId)
    {
        return await taskRepository.RemoveTaskAsync(taskId);
    }

    public async Task<bool> AddCommentAsync(int taskId, CommentRequest request)
    {
        var task = await taskRepository.GetTaskByIdAsync(taskId);
        if (task == null)
            throw new Exception("Tarefa não encontrada.");

        var comment = new Comment
        {
            TaskId = taskId,
            Content = request.Content,
            CommentedAt = DateTime.UtcNow,
            CommentedBy = "Usuário Teste"
        };

        task.Comments.Add(comment);

        var history = new TaskHistory
        {
            TaskId = taskId,
            ChangeDescription = "Comentário adicionado.",
            ModifiedAt = DateTime.UtcNow,
            ModifiedBy = comment.CommentedBy
        };

        task.Histories.Add(history);

        return await taskRepository.UpdateTaskAsync(task);
    }

    public async Task<bool> AddTaskHistoryAsync(int taskId, string changeDescription, string modifiedBy)
    {
        var task = await taskRepository.GetTaskByIdAsync(taskId);
        if (task == null)
            return false;

        var history = new TaskHistory
        {
            TaskId = taskId,
            ChangeDescription = changeDescription,
            ModifiedAt = DateTime.UtcNow,
            ModifiedBy = modifiedBy
        };

        task.Histories.Add(history);

        return await taskRepository.UpdateTaskAsync(task);
    }

    public async Task<double> GetAvergeCompletedTasks()
    {
        return await taskRepository.GetAvergeCompletedTasks();
    }
}
