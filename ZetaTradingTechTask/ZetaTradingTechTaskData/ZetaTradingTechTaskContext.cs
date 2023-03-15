using Microsoft.EntityFrameworkCore;
using ZetaTradingTechTaskData.Models;

namespace ZetaTradingTechTaskData;

public class ZetaTradingTechTaskContext : DbContext
{
    public DbSet<Node> Nodes { get; set; }
    
    public DbSet<Tree> Trees { get; set; }
    
    //public DbSet<ServerException> Exceptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=zeta_db;Username=admin;Password=1234");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIdentityColumns();
        base.OnModelCreating(modelBuilder);
    }
}