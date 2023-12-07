using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetNetApple.Models;
using System.Linq;
using System.Threading.Tasks;

public class UsersController : Controller
{
    private readonly AppleDbContext _context;

    public UsersController(AppleDbContext context)
    {
        _context = context;
    }

    // GET: Usersses
    public async Task<IActionResult> Index()
    {
        var users = await _context.Usersses.ToListAsync();
        return View(users);
    }

    // GET: Usersses/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Usersses
            .FirstOrDefaultAsync(m => m.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    // GET: Usersses/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Usersses/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Userss user)
    {
        if (ModelState.IsValid)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(user);
    }

    // GET: Usersses/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Usersses.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    // POST: Usersses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Userss user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
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

        return View(user);
    }

    // GET: Usersses/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Usersses
            .FirstOrDefaultAsync(m => m.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    // POST: Usersses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _context.Usersses.FindAsync(id);
        _context.Usersses.Remove(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UserExists(int id)
    {
        return _context.Usersses.Any(e => e.Id == id);
    }
}
