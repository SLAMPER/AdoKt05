using AdoKt05.Data;
using AdoKt05.Dto;
using AdoKt05.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdoKt05.Api;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IRepository<ProductEntity, Guid> _repository;

	public ProductsController(IUnitOfWork unitOfWork, IRepository<ProductEntity, Guid> repository)
	{
		_unitOfWork = unitOfWork;
		_repository = repository;
	}

	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
	public async Task<IActionResult> GetAll()
	{
		var products = await _repository.GetAllAsync();
		var dtos = products
			.Select(x =>
				new ProductDto(x.Id, x.Name, x.Price, x.FkCategoryId))
			.ToList();
		return Ok(dtos);
	}

	[HttpGet("{id:guid}")]
	[ProducesResponseType(typeof(ProductDto), 200)]
	[ProducesResponseType(404)]
	public async Task<IActionResult> GetById(Guid id)
	{
		var product = await _repository.GetByIdAsync(id);

		if (product == null)
		{
			return NotFound();
		}

		var dto = new ProductDto(product.Id, product.Name, product.Price, product.FkCategoryId);

		return Ok(dto);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] ProductDto product)
	{
		_repository.Create(new ProductEntity
		{
			Id = product.Id,
			Name = product.Name,
			Price = product.Price,
			FkCategoryId = product.FkCategoryId,
		});
		await _unitOfWork.SaveAsync();

		return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
	}

	[HttpPut("{id:guid}")]
	[ProducesResponseType(204)]
	[ProducesResponseType(404)]
	public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto product)
	{
		if (id != product.Id)
		{
			return BadRequest();
		}

		_repository.Update(new ProductEntity
		{
			Id = product.Id,
			Name = product.Name,
			Price = product.Price,
			FkCategoryId = product.FkCategoryId,
		});
		await _unitOfWork.SaveAsync();

		return NoContent();
	}

	[HttpDelete("{id:guid}")]
	[ProducesResponseType(204)]
	[ProducesResponseType(404)]
	public async Task<IActionResult> Delete(Guid id)
	{
		var product = await _repository.GetByIdAsync(id);
		if (product == null)
		{
			return NotFound();
		}

		await _repository.DeleteAsync(id);
		await _unitOfWork.SaveAsync();
		return NoContent();
	}
}
