using System.ComponentModel.DataAnnotations;

namespace AdoKt05.Data;

public class ProductEntity
{
	[Key] public required Guid Id { get; init; }
	public string Name { get; set; } = "";
	public double Price { get; set; }

	public required Guid FkCategoryId { get; init; }
	public CategoryEntity CategoryEntity { get; set; } = null!;
}
