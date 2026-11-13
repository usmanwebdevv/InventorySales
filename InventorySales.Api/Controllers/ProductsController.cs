using System;
using InventorySales.Application.Interfaces;
using InventorySales.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventorySales.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }
    [HttpGet]
    public async Task <IActionResult> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult>GetById(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if(product == null) return NotFound();
        return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var created = await _repository.AddAsync(product);
        return CreatedAtAction(nameof(GetById),new {id = created.Id}, created);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id , Product product)
    {
        if(id != product.Id) return BadRequest();
        await _repository.UpdateAsync(product);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
