using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Models;
using TaskManagement.Core.Services;

namespace TaskManagementApi.Endpoints;

public class RemoveProjectEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/{projectId}", Handle);
    }

    private static async Task<IResult> Handle([FromServices] IProjectService service, int projectId)
    {
        try
        {
            if (await service.HasPendingTasksAsync(projectId))
            {
                return Results.BadRequest("Não é possível remover o projeto com tarefas pendentes ou em andamento.");
            }

            var result = await service.RemoveProjectAsync(projectId);
            return !result ? Results.NotFound("Projeto nao encontrado") : Results.NoContent();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}