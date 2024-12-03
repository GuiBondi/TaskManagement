using TaskManagement.Core.Models;

namespace TaskManagement.Core.Repository;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<Project> GetProjectByIdAsync(int projectId);
    Task<Project> AddProjectAsync(Project project);
    Task<bool> RemoveProjectAsync(int projectId);
    Task<bool> HasPendingTasksAsync(int projectId);
}