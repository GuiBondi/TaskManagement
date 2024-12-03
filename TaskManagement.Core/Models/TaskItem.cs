namespace TaskManagement.Core.Models;

public class TaskItem
{
    public TaskItem()
    {
        Histories = new List<TaskHistory>();
        Comments = new List<Comment>();
    }

    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Status { get; set; } 
    public int Priority { get; set; }
    public ICollection<TaskHistory> Histories { get; set; }
    public ICollection<Comment> Comments { get; set; }
}

