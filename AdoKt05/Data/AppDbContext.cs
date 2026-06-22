using Microsoft.EntityFrameworkCore;

namespace AdoKt05.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<ProductEntity> Products { get; set; }
	public DbSet<CategoryEntity> Categories { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<ProductEntity>()
			.HasOne(e => e.CategoryEntity)
			.WithMany(e => e.ProductEntities)
			.HasForeignKey(e => e.FkCategoryId)
			.IsRequired();
	}
}
