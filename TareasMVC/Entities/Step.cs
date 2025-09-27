using System.ComponentModel.DataAnnotations;

namespace TareasMVC.Entities;

public class Step
{
    public Guid Id { get; set; }

    public int TaskId { get; set; }
    public required Task Task { get; set; }

    [Required]
    [StringLength(256)]
    public required string Description { get; set; }

    public bool Done { get; set; }
    public int Order { get; set; }
}