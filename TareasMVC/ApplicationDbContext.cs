using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Entities;

namespace TareasMVC;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Entities.Task> Tasks { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<FileAttachment> FileAttachments { get; set; }
}