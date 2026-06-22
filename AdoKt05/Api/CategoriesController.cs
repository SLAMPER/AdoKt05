using AdoKt05.Data;
using AdoKt05.Dto;
using AdoKt05.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdoKt05.Api;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IRepository<CategoryEntity, Guid> _repository;

	public CategoriesController(IUnitOfWork unitOfWork, IRepository<CategoryEntity, Guid> repository)
	{
		_unitOfWork = unitOfWork;
		_repository = repository;
	}

	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
	public async Task<IActionResult> GetAll()
	{
		var categories = await _repository.GetAllAsync();
		var dtos = categories
			.Select(x =>
				new CategoryDto(x.Id, x.Name))
			.ToList();
		return Ok(dtos);
	}

	[HttpGet("{id:guid}")]
	[ProducesResponseType(typeof(CategoryDto), 200)]
	[ProducesResponseType(404)]
	public async Task<IActionResult> GetById(Guid id)
	{
		var category = await _repository.GetByIdAsync(id);

		if (category == null)
		{
			return NotFound();
		}

		var dto = new CategoryDto(category.Id, category.Name);

		return Ok(dto);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CategoryDto category)
	{
		_repository.Create(new CategoryEntity
		{
			Id = category.Id,
			Name = category.Name,
		});
		await _unitOfWork.SaveAsync();

		return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
	}

	[HttpPut("{id:guid}")]
	[ProducesResponseType(204)]
	[ProducesResponseType(404)]
	public async Task<IActionResult> Update(Guid id, [FromBody] CategoryDto category)
	{
		if (id != category.Id)
		{
			return BadRequest();
		}

		_repository.Update(new CategoryEntity
		{
			Id = category.Id,
			Name = category.Name,
		});
		await _unitOfWork.SaveAsync();

		return NoContent();
	}

	[HttpDelete("{id:guid}")]
	[ProducesResponseType(204)]
	[ProducesResponseType(404)]
	public async Task<IActionResult> Delete(Guid id)
	{
		var category = await _repository.GetByIdAsync(id);
		if (category == null)
		{
			return NotFound();
		}

		await _repository.DeleteAsync(id);
		await _unitOfWork.SaveAsync();
		return NoContent();
	}
}
