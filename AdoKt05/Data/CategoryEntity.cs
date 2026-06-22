using System.ComponentModel.DataAnnotations;

namespace AdoKt05.Data;

public class CategoryEntity
{
	[Key] public required Guid Id { get; init; }
	public string Name { get; set; } = "";

	public ICollection<ProductEntity> ProductEntities { get; set; } = null!;
}
