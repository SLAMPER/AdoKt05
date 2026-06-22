using Microsoft.EntityFrameworkCore;

namespace AdoKt05.Data.Seeder;

public static class AppDbContextSeeder
{
	public static async Task SeedDbAsync(DbContext context, CancellationToken ct = default)
	{
		var persistantGuidTestCategory = new Guid("00000000-0000-0000-0000-000000000002");
		var testCategory = await context.Set<CategoryEntity>()
			.FirstOrDefaultAsync(x => x.Id == persistantGuidTestCategory, ct);
		if (testCategory == null)
		{
			context.Set<CategoryEntity>()
				.Add(new CategoryEntity
				{
					Id = persistantGuidTestCategory,
					Name = "SomeCategory",
				});
		}

		var persistantGuidTestProduct = new Guid("00000000-0000-0000-0000-000000000001");
		var testProduct = await context.Set<ProductEntity>()
			.FirstOrDefaultAsync(x => x.Id == persistantGuidTestProduct, ct);
		if (testProduct == null)
		{
			context.Set<ProductEntity>()
				.Add(new ProductEntity
					{
						Id = persistantGuidTestProduct,
						Name = "NameIt",
						Price = 6_666,
						FkCategoryId = persistantGuidTestCategory,
					}
				);
		}

		await context.SaveChangesAsync(ct);
	}
}
