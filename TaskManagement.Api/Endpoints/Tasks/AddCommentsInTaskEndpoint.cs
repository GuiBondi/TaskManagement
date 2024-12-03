using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Requests;
using TaskManagement.Core.Services;

namespace TaskManagementApi.Endpoints.Tasks;

public class AddCommentsInTaskEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/Comment", Handle);
    }

    private static async Task<IResult> Handle([FromServices] ITaskService service, int taskId, 
        [FromBody] CommentRequest comment)
    {
        try
        {
            var result = await service.AddCommentAsync(taskId, comment);
            return result ? Results.Created($"/tasks/{taskId}/comments/", comment) : Results.NotFound("Tarefa não encontrada.");
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}