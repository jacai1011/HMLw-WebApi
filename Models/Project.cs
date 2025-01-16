namespace HMLw_WebApi.Models;
public class Project
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string Color { get; set; } = "#FFFFFF";
    public ICollection<TaskList> TaskLists { get; } = [];
}