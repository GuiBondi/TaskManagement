using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Models;
using TaskManagement.Core.Services;

namespace TaskManagementApi.Endpoints;

public class GetAllProjects: IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", Handle);
    }

    private static async Task<IEnumerable<Project>> Handle([FromServices] IProjectService service)
    {
        try
        {
            return await service.GetAllProjectsAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}