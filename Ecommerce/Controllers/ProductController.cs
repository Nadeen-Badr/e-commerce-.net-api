using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceApi.DTOs;
using ECommerceApi.Models;
using ECommerceApi.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "SellerOnly")] // Only sellers can access these endpoints
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository productRepository,IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        Console.WriteLine($"User ID: {userId}");
        Console.WriteLine($"User Role: {userRole}");

        var products = await _productRepository.GetAllAsync();
        var productDtos = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        return Ok(productDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDTO createProductDTO)
    {
        if (!ModelState.IsValid)
        return BadRequest(ModelState);

        var sellerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine($"SellerId: {sellerId}");

        if (string.IsNullOrEmpty(sellerId))
        return Unauthorized("User not authenticated.");
        var product = new Product
        {
            Name = createProductDTO.Name,
            Description = createProductDTO.Description,
            Price = createProductDTO.Price,
            Stock = createProductDTO.Stock,
            SellerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value // Set the SellerId to the current user's ID
        };

        await _productRepository.AddAsync(product);
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDTO updateProductDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null || existingProduct.SellerId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            return Forbid();

        existingProduct.Name = updateProductDTO.Name;
        existingProduct.Description = updateProductDTO.Description;
        existingProduct.Price = updateProductDTO.Price;
        existingProduct.Stock = updateProductDTO.Stock;

        await _productRepository.UpdateAsync(existingProduct);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null || product.SellerId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            return Forbid();

        await _productRepository.DeleteAsync(id);
        return NoContent();
    }
[HttpGet("my-products")]
public async Task<IActionResult> GetMyProducts()
{
    // Get the current user's ID
    var sellerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (string.IsNullOrEmpty(sellerId))
        return Unauthorized("User not authenticated.");

    // Fetch products created by the current seller
    var products = await _productRepository.GetProductsBySellerIdAsync(sellerId);
    return Ok(products);
}
}