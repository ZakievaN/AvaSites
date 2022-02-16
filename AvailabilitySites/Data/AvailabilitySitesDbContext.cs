using Microsoft.EntityFrameworkCore;

namespace AvailabilitySites.Data;

public class AvailabilitySitesDbContext : DbContext
{
    public AvailabilitySitesDbContext(DbContextOptions<AvailabilitySitesDbContext> options)
    : base(options)
    {
    }

    public DbSet<Site> Sites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Site>(builder =>
        {
            builder.ToTable("Sites");
            builder.HasKey(x => x.Id);
        });
    }
}