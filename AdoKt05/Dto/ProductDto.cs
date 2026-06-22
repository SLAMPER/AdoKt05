namespace AdoKt05.Dto;

public record ProductDto(
	Guid Id,
	string Name,
	double Price,
	Guid FkCategoryId
);
