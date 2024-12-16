using Microsoft.EntityFrameworkCore;
using WebApis.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Add DbSet properties for your entities
    public DbSet<RankBonus> RankBonus { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define the RankBonus entity as keyless
        modelBuilder.Entity<RankBonus>().HasNoKey();

        base.OnModelCreating(modelBuilder);
    }
}
