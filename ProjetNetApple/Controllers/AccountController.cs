using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetNetApple.Models;
using ProjetNetApple.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly AppleDbContext _context;

    public AccountController(AppleDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        // Validate credentials and sign in logic...

        // Get user's actual name from your user data (replace this with your actual logic)
        var userName = GetUserNameByEmail(model.Email);

        // Create claims for the user
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userName),
        new Claim(ClaimTypes.Name, userName),
        // Add more claims as needed
    };

        // Create identity
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Sign in the user
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            RedirectUri = "products/index"
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToAction("Index", "Home");
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }

    private string GetUserNameByEmail(string email)
    {
        // Replace this with your actual data access logic
            // context
            using (var context = new AppleDbContext())
        {
            var user = context.Usersses.FirstOrDefault(u => u.Email == email);

            // Check if the user was found
            if (user != null)
            {
                return user.Fname;
            }

            // If the user was not found, you might return a default or handle it as needed
            return "DefaultUserName";
        }
    }

}
