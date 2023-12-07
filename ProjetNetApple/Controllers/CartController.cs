using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetNetApple.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class CartController : Controller
{
    private readonly AppleDbContext _context;

    public CartController(AppleDbContext context)
    {
        _context = context;
    }
    public IActionResult AddToCart(int productId, int quantity)
    {
        // Example: Add product to CartLines
        // Note: You may also need to update your view accordingly
        var cartLine = new CartLine
        {
            CartId = GetCurrentCartId(),  // Implement this method to get the current cart ID
            ProductId = productId,
            Quantity = quantity
        };

        _context.CartLines.Add(cartLine);
        _context.SaveChanges();

        return RedirectToAction("Index", "Home");  // Redirect to the home page or wherever appropriate
    }

    private int GetCurrentCartId()
    {
        // Retrieve the currently logged-in user (you may need to adapt this based on your authentication system)
        var currentUser = HttpContext.User;

        if (currentUser != null)
        {
            // Get the user ID (replace "GetUserId" with the actual method to get the user ID)
            var userId = GetUserId(currentUser);

            // Retrieve the cart ID associated with the user from the database
            var userCart = _context.Carts.FirstOrDefault(c => c.UserId == userId);

            if (userCart != null)
            {
                return userCart.Id;
            }
            else
            {
                // Create a new cart if the user doesn't have one
                var newCart = new Cart { UserId = userId };
                _context.Carts.Add(newCart);
                _context.SaveChanges();

                return newCart.Id;
            }
        }

        // Return a default or handle the case where there is no user
        return 0;
    }

    private int GetUserId(ClaimsPrincipal user)
    {
        // Find the claim representing the user ID
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            // Convert the user ID to an integer (assuming it's an integer)
            return userId;
        }

        // Return a default or handle the case where the user ID is not present or not an integer
        return 0;
    }

    // GET: Carts
    public async Task<IActionResult> Index()
    {
        var carts = await _context.Carts.ToListAsync();
        return View(carts);
    }

    // GET: Carts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cart = await _context.Carts
            .FirstOrDefaultAsync(m => m.Id == id);

        if (cart == null)
        {
            return NotFound();
        }

        return View(cart);
    }

    // GET: Carts/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Carts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cart cart)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(cart);
    }

    // GET: Carts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cart = await _context.Carts.FindAsync(id);
        if (cart == null)
        {
            return NotFound();
        }

        return View(cart);
    }

    // POST: Carts/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Cart cart)
    {
        if (id != cart.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cart);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(cart.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        return View(cart);
    }

    // GET: Carts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cart = await _context.Carts
            .FirstOrDefaultAsync(m => m.Id == id);

        if (cart == null)
        {
            return NotFound();
        }

        return View(cart);
    }

    // POST: Carts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cart = await _context.Carts.FindAsync(id);
        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CartExists(int id)
    {
        return _context.Carts.Any(e => e.Id == id);
    }
}
