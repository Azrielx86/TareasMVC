using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TareasMVC.Entities;

public class FileAttachment
{
    public Guid Id { get; set; }
    public int TaskId { get; set; }
    public required Task Task { get; set; }

    [Unicode]
    [Required]
    [StringLength(2048)]
    public required string Url { get; set; }

    [Required]
    [StringLength(64)]
    public required string Title { get; set; }

    public int Order { get; set; }
    public DateTime CreationDate { get; set; }
}