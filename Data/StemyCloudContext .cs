using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class StemyCloudContext : DbContext
{
    public DbSet<CloudFile> Files { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=StemyCloudDB;Username=postgre;Password=12345");
    }
}
