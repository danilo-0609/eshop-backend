using Catalog.Domain.Comments;
using Catalog.Domain.Products;
using Catalog.Domain.Ratings;
using Catalog.Domain.Sales;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }

    DbSet<Comment> Comments { get; set; }

    DbSet<Rating> Ratings { get; set; }

    DbSet<Sale> Sales { get; set; }
}   