namespace HMLw_WebApi.Models;
public class TaskList
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public long ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public ICollection<Task> Tasks { get; } = [];
}