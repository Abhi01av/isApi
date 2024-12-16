using Microsoft.EntityFrameworkCore;
using WebApis.Models;

namespace WebApis.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Add DbSet properties for your entities
        public DbSet<RankBonus> RankBonuses { get; set; }
    }
}
