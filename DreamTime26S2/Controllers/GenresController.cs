
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DreamTime26S2.Models;
using DreamTime26S2.Data;

public class GenresController : Controller
{
    private readonly ApplicationDbContext _context;

    public GenresController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: GENRES
    public async Task<IActionResult> Index()    
    {
        // OPTION 1: Just return ALL books with each genre (very expensive/inefficient)
        //var genres = await _context.Genres.Include(g => g.Books).ToListAsync();

        // OPTION 2: Check if each genre has books - update Genre.HasBooks property
        var genres = await _context.Genres
            .Select(g => new Genre
            {
                Id = g.Id,
                Name = g.Name,
                HasBooks = g.Books.Any()
            })
            .ToListAsync();

        return View(genres);
    }

    // GET: GENRES/Details/5
    //public async Task<IActionResult> Details(int? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var genre = await _context.Genres
    //        .FirstOrDefaultAsync(m => m.Id == id);
    //    if (genre == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(genre);
    //}

    // GET: GENRES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: GENRES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Genre genre)
    {
        if (ModelState.IsValid)
        {
            _context.Add(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(genre);
    }

    // GET: GENRES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }
        return View(genre);
    }

    // POST: GENRES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name")] Genre genre)
    {
        if (id != genre.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(genre);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(genre.Id))
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
        return View(genre);
    }

    // GET: GENRES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await _context.Genres
            .FirstOrDefaultAsync(m => m.Id == id);
        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    // POST: GENRES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var genre = await _context.Genres.FindAsync(id);

        // Check if genre not found
        if (genre == null) return NotFound("Genre not found.");

        // Check if genre has books - stop deletion (add error)
        if (_context.Books.Any(b => b.GenreId == id))
        {
            ModelState.AddModelError("Id", "Cannot delete genre because it has associated books.");
            return View(genre);
        }

        if (genre != null)
        {
            _context.Genres.Remove(genre);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GenreExists(int? id)
    {
        return _context.Genres.Any(e => e.Id == id);
    }
}
