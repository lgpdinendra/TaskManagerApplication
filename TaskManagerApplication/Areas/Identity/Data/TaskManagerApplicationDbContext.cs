using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagerApplication.Areas.Identity.Data;
using TaskManagerApplication.Models;

namespace TaskManagerApplication.Areas.Identity.Data;

public class TaskManagerApplicationDbContext : IdentityDbContext<TaskManagerApplicationUser>
{
    public TaskManagerApplicationDbContext(DbContextOptions<TaskManagerApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Tasks> Tasks { get; set; }
}
