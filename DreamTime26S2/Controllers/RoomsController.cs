
using DreamTime26S2.Data;
using DreamTime26S2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

[Route("[controller]/[action]/{code?}")]
public class RoomsController : Controller
{
    private readonly ApplicationDbContext _context;

    public RoomsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Rooms/
    public async Task<IActionResult> Index()
    {
        return View(await _context.Rooms.Include(r => r.RoomType).OrderBy(r => r.Code).ToListAsync());
    }

    // GET: Rooms/Details/5
    public async Task<IActionResult> Details(string? code)
    {
        if (code == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms
            .FirstOrDefaultAsync(m => m.Code == code);
        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    // GET: Rooms/Create
    public IActionResult Create()
    {
        // Get data and load the view
        ViewData["RoomTypeList"] = new SelectList(_context.RoomTypes, "Id", "Name");
        return View();
    }

    // POST: Rooms/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Code,RoomTypeId,RoomType")] Room room)
    {
        if (ModelState.IsValid)
        {
            _context.Add(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Get data and load the view
        ViewData["RoomTypeList"] = new SelectList(_context.RoomTypes, "Id", "Name");
        return View(room);
    }

    // GET: Rooms/Edit/5
    public async Task<IActionResult> Edit(string? code)
    {
        if (code == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms.FindAsync(code);
        if (room == null)
        {
            return NotFound();
        }

        // Get data and load the view
        ViewData["RoomTypeList"] = new SelectList(_context.RoomTypes, "Id", "Name");
        return View(room);
    }

    // POST: Rooms/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? code, [Bind("Code,RoomTypeId,RoomType")] Room room)
    {
        if (code != room.Code)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(room);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(room.Code))
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

        // Get data and load the view
        ViewData["RoomTypeList"] = new SelectList(_context.RoomTypes, "Id", "Name");
        return View(room);
    }

    // GET: Rooms/Delete/5
    public async Task<IActionResult> Delete(string? code)
    {
        if (code == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms
            .FirstOrDefaultAsync(m => m.Code == code);
        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    // POST: Rooms/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? code)
    {
        var room = await _context.Rooms.FindAsync(code);
        if (room != null)
        {
            _context.Rooms.Remove(room);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RoomExists(string? code)
    {
        return _context.Rooms.Any(e => e.Code == code);
    }
}
