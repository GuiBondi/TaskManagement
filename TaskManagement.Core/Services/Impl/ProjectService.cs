using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Requests;
using TaskManagement.Core.Repository;

namespace TaskManagement.Core.Services.Impl;

public class ProjectService(IProjectRepository repository) : IProjectService
{
    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        return await repository.GetAllProjectsAsync();
    }

    public async Task<Project> GetProjectByIdAsync(int projectId)
    {
        return await repository.GetProjectByIdAsync(projectId);
    }

    public async Task<Project> AddProjectAsync(CreateProjectRequest request)
    {
        var project = new Project
        {
            Name = request.Name
        };
        return await repository.AddProjectAsync(project);
    }

    public async Task<bool> RemoveProjectAsync(int projectId)
    {
        return await repository.RemoveProjectAsync(projectId);
    }

    public async Task<bool> HasPendingTasksAsync(int projectId)
    {
        return await repository.HasPendingTasksAsync(projectId);
    }
}
