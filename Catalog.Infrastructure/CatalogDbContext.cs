using Catalog.Application.Common;
using Catalog.Domain.Comments;
using Catalog.Domain.Products;
using Catalog.Domain.Ratings;
using Catalog.Domain.Sales;
using Catalog.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure;

public partial class CatalogDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IConfiguration _configuration;

    public CatalogDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Sale> Sales { get; set; }

    internal DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OutboxMe__3214EC070737DE5A");

            entity.ToTable("OutboxMessages", "catalog");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Content)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.Error)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.OcurredOnUtc).HasColumnType("datetime");
            entity.Property(e => e.ProcessedOnUtc).HasColumnType("datetime");
            entity.Property(e => e.Type)
                .HasMaxLength(300)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Database"));
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public CatalogDbContext(){}
}