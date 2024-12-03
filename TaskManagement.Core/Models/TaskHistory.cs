namespace TaskManagement.Core.Models;

public class TaskHistory
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string ChangeDescription { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; }
}
