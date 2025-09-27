using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TareasMVC.Entities;

public class Task
{
    public int Id { get; set; }

    [StringLength(64)]
    [Required]
    public required string Title { get; set; }

    [StringLength(1024)]
    public string? Description { get; set; }

    public int Order { get; set; }
    public DateTime CreationDate { get; set; }

    [StringLength(450)]
    public required string UserCreationId { get; set; }
    public IdentityUser? UserCreation { get; set; }
}