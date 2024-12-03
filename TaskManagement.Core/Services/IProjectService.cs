using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Requests;

namespace TaskManagement.Core.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<Project> GetProjectByIdAsync(int projectId);
    Task<Project> AddProjectAsync(CreateProjectRequest project);
    Task<bool> RemoveProjectAsync(int projectId);
    Task<bool> HasPendingTasksAsync(int projectId);
}