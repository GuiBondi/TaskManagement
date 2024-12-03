using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Models;
using TaskManagement.Core.Services;

namespace TaskManagementApi.Endpoints.Reports;

public class GetAverageTasksCompletedEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", Handle);
    }

    private static async Task<IResult> Handle(
        [FromServices] ITaskService service, 
        [FromHeader(Name = "X-User-Role")] string? userRole)
    {
        if (string.IsNullOrWhiteSpace(userRole) || userRole.ToLower() != "manager")
        {
            return Results.StatusCode(401);
        }
        try
        {
            var average = await service.GetAvergeCompletedTasks();
            return Results.Ok($"A media de tarefas concluidas e: {average}");
        }
        catch (Exception ex)
        {
            return Results.NoContent();
        }
    }
}