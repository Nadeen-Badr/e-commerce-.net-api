using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Models;
using ECommerceApi.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "BuyerOnly")] // Only buyers can access these endpoints
public class OrderController : ControllerBase
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    public OrderController(ICartRepository cartRepository, IOrderRepository orderRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

  [HttpPost("place-order")]
public async Task<IActionResult> PlaceOrder()
{
    var buyerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (string.IsNullOrEmpty(buyerId))
        return Unauthorized("User not authenticated.");

    // Fetch the buyer's cart items
    var cartItems = await _cartRepository.GetCartItemsAsync(buyerId);

    if (cartItems == null || !cartItems.Any())
        return BadRequest("Your cart is empty.");

    // Create a new order
    var order = new Order
    {
        BuyerId = buyerId,
        OrderDate = DateTime.UtcNow,
        TotalAmount = cartItems.Sum(ci => ci.Product.Price * ci.Quantity),
        OrderItems = cartItems.Select(ci => new OrderItem
        {
            ProductId = ci.ProductId,
            Quantity = ci.Quantity,
            Price = ci.Product.Price
        }).ToList()
    };

    // Add the order to the database
    await _orderRepository.AddOrderAsync(order);

    // Clear the buyer's cart
    await _cartRepository.ClearCartAsync(buyerId);

    // Map the order to OrderResponseDTO
    var orderResponse = _mapper.Map<OrderResponseDTO>(order);

    return Ok(orderResponse);
}
[HttpGet("order-history")]
public async Task<IActionResult> GetOrderHistory()
{
    var buyerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (string.IsNullOrEmpty(buyerId))
        return Unauthorized("User not authenticated.");

    var orders = await _orderRepository.GetOrderHistoryAsync(buyerId);
    var orderDtos = _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);

    return Ok(orderDtos);
}
}