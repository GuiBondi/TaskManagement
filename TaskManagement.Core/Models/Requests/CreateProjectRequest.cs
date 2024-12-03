namespace TaskManagement.Core.Models.Requests;

public struct CreateProjectRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}