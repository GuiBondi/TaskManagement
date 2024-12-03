using TaskManagement.Core.Models.Enums;

namespace TaskManagement.Core.Models.Requests;

public struct TaskRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskItemStatus Status { get; set; }
    public Priority Priority { get; set; }
}