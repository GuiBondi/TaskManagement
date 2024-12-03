using TaskManagementApi.Endpoints;
using TaskManagementApi.Endpoints.Reports;
using TaskManagementApi.Endpoints.Tasks;

namespace TaskManagementApi.Commom.Extensions;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("api/");

        group.MapGroup("projects")
            .WithTags("projects")
            .MapEndpoint<GetAllProjectsEndpoint>()
            .MapEndpoint<RemoveProjectEndpoint>()
            .MapEndpoint<CreateProjectEndpoint>();

        group.MapGroup("tasks")
            .WithTags("tasks")
            .MapEndpoint<CreateTasksEndpoint>()
            .MapEndpoint<GetTasksFromProjectEndpoint>()
            .MapEndpoint<UpdateTasksEndpoint>()
            .MapEndpoint<AddCommentsInTaskEndpoint>();

        group.MapGroup("reports")
            .WithTags("reports")
            .MapEndpoint<GetAverageTasksCompletedEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}