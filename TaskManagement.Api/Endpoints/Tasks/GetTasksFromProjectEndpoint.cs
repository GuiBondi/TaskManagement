using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Models;
using TaskManagement.Core.Services;

namespace TaskManagementApi.Endpoints.Tasks;

public class GetTasksFromProjectEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{projectId}/tasks", Handle);
    }

    private static async Task<IResult> Handle([FromServices] ITaskService service, int projectId)
    {
        try
        {
            return Results.Ok(await service.GetTasksByProjectIdAsync(projectId));
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
