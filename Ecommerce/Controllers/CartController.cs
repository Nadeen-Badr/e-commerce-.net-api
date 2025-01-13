using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Models;
using ECommerceApi.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ECommerceApi.DTOs;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "BuyerOnly")] // Only buyers can access these endpoints
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CartController(ICartRepository cartRepository,IProductRepository productRepository,IMapper mapper)
    {
        _cartRepository = cartRepository;
          _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetCartItems()
    {
        var buyerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(buyerId))
            return Unauthorized("User not authenticated.");

        var cartItems = await _cartRepository.GetCartItemsAsync(buyerId);
         var cartItemDtos = _mapper.Map<IEnumerable<CartResponseDTO>>(cartItems);
        return Ok(cartItemDtos);
    }

  [HttpPost]
public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO addToCartDTO)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var buyerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (string.IsNullOrEmpty(buyerId))
        return Unauthorized("User not authenticated.");

    // Create a new Cart object from the DTO
    var cart = new Cart
    {
        BuyerId = buyerId,
        ProductId = addToCartDTO.ProductId,
        Quantity = addToCartDTO.Quantity
    };

    await _cartRepository.AddToCartAsync(cart);
    var cartItem = await _cartRepository.GetCartItemByIdAsync(cart.Id);
    var cartItemDto = _mapper.Map<CartResponseDTO>(cartItem);
    return Ok(cartItemDto);
}

    [HttpDelete("{cartItemId}")]
    public async Task<IActionResult> RemoveFromCart(int cartItemId)
    {
        var buyerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(buyerId))
            return Unauthorized("User not authenticated.");

        await _cartRepository.RemoveFromCartAsync(cartItemId);
        return NoContent();
    }
    [HttpGet("all products")]
            public async Task<IActionResult> GetAll()
    {
    
        var products = await _productRepository.GetAllAsync();
        var productDtos = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        return Ok(productDtos);
    }
}