using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class StemyCloudContext : DbContext
{
    public StemyCloudContext(DbContextOptions<StemyCloudContext> options) : base(options) { }

    public DbSet<CloudFile> Files { get; set; }
    public DbSet<User> Users { get; set; }
}
