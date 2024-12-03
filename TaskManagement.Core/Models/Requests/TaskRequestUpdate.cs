using TaskManagement.Core.Models.Enums;

namespace TaskManagement.Core.Models.Requests;

public struct TaskRequestUpdate
{
    public string Description { get; set; }
    public TaskItemStatus Status { get; set; }
}