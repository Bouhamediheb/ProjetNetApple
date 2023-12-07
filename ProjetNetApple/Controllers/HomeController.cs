using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetNetApple.Models;
using System.Linq;


[AllowAnonymous]
public class HomeController : Controller
{
    private readonly AppleDbContext _context;

    public HomeController(AppleDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Retrieve best-selling products (replace this with your actual logic)
        var bestSellingProducts = _context.Products.Take(3).ToList();

        // Pass the best-selling products to the view
        return View(bestSellingProducts);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId)
    {
        // Retrieve the current user ID (replace this with your actual logic to get the user ID)
        var userId = GetCurrentUserId();

        // Check if the product is already in the cart
        var existingCartItem = _context.Carts.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

        if (existingCartItem != null)
        {
            // If the product is already in the cart, increment the quantity
            existingCartItem.Quantity++;
        }
        else
        {
            // If the product is not in the cart, add a new cart item
            var newCartItem = new Cart
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };

            _context.Carts.Add(newCartItem);
        }

        _context.SaveChanges();

        // Redirect back to the home page or the cart page
        return RedirectToAction("Index");
    }

    private int GetCurrentUserId()
    {
        // Replace this with your actual logic to get the current user ID
        // Example: return HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return 1; // Placeholder value; replace with your logic
    }
}
