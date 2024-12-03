using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Requests;
using TaskManagement.Core.Services;

namespace TaskManagementApi.Endpoints.Tasks;

public class UpdateTasksEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/", Handle);
    }

    private static async Task<IResult> Handle([FromServices] ITaskService service, int taskId, [FromBody] TaskRequestUpdate updatedTask)
    {
        try
        {
            var result = await service.UpdateTaskAsync(taskId, updatedTask);
            return result ? Results.Ok(updatedTask) : Results.NotFound("Tarefa não encontrada.");
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}