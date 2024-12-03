using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Requests;
using TaskManagement.Core.Services;

namespace TaskManagementApi.Endpoints.Tasks;

public class CreateTasksEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/{projectId}", Handle);
    }

    private static async Task<IResult> Handle([FromServices] ITaskService service, int projectId,[FromBody] TaskRequest task)
    {
        try
        {
            var createdTask = await service.AddTaskAsync(projectId, task);
            return Results.Created($"/projects/{projectId}/tasks/{createdTask.Id}", createdTask);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}