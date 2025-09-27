namespace TareasMVC.Entities;

public class TaskDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
}