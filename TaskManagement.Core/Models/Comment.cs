namespace TaskManagement.Core.Models;

public class Comment
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string Content { get; set; }
    public DateTime CommentedAt { get; set; }
    public string CommentedBy { get; set; }
}
