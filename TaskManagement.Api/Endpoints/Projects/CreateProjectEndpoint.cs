using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Models;
using TaskManagement.Core.Models.Requests;
using TaskManagement.Core.Services;

namespace TaskManagementApi.Endpoints;

public class CreateProjectEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/", Handle);
    }

    private static async Task<IResult> Handle([FromServices] IProjectService service, [FromBody] CreateProjectRequest project)
    {
        try
        {
            
            var createdProject = await service.AddProjectAsync(project);
            return Results.Created($"projects/{createdProject.Id}", createdProject);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}