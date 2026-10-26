using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DreamTime26S2.Models;
using DreamTime26S2.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

[Route("[controller]/[action]/{isbn?}")]
public class BooksController : Controller
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Books
    public async Task<IActionResult> Index()
    {
        // Include genres with books
        return View(await _context.Books.Include(b => b.Genre).ToListAsync());
    }

    // GET: Books/Details/5
    public async Task<IActionResult> Details(string? isbn)
    {
        if (isbn == null) return BadRequest("Must provide an ISBN.");

        // Get book with genre
        var book = await _context.Books
            .Include(b => b.Genre)
            .FirstOrDefaultAsync(b => b.ISBN == isbn);

        if (book == null) return NotFound("Book not found.");

        return View(book);
    }

    // GET: Books/Create
    public IActionResult Create()
    {
        // Get a list of genres from the DB
        ViewData["GenreList"] = new SelectList(_context.Genres, "Id", "Name");
        return View();
    }

    // POST: Books/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ISBN,Title,Author,Pages,Description,ImageFilename,Copies,Publisher,DatePublished,Edition,Genre,GenreId")] Book book)
    {
        // Turn genre ID into a Genre object
        Genre? genre = await _context.Genres.FindAsync(book.GenreId);

        // Check if genre does not exist and throw 404 error (or something nicer)
        if (genre == null)
        {
            // Return 404 error (very bad UX)
            //return NotFound("Oi, cut it out! Stop messing with the genre ID!");

            // Nicer option: return a custom validation error message
            ModelState.AddModelError("GenreId", "Genre doesn't exist.");
        }

        //// OPTION 1: Assign genre object to the book and then revalidate the model
        //book.Genre = genre;
        //ModelState.Clear();
        //TryValidateModel(book);

        // OPTION 2: Remove/ignore validation for ONLY the Genre property
        ModelState.Remove("Genre");

        if (ModelState.IsValid)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Get a list of genres from the DB
        ViewData["GenreList"] = new SelectList(_context.Genres, "Id", "Name");
        return View(book);
    }

    // GET: Books/Edit/5
    //[HttpGet("/[controller]/[action]/{isbn?}")]
    public async Task<IActionResult> Edit(string? isbn)
    {
        // No ID, no result
        if (isbn == null) return BadRequest("Must provide an ISBN.");

        // Look up book in DB
        var book = await _context.Books.FindAsync(isbn);

        // Check if ISBN exists
        if (book == null) return NotFound("ISBN not found.");

        // Get a list of genres from the DB
        ViewData["GenreList"] = new SelectList(_context.Genres, "Id", "Name");

        // Display form with book data
        return View(book);
    }

    // POST: Books/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? isbn, [Bind("ISBN,Title,Author,Pages,Description,ImageFilename,Copies,Publisher,DatePublished,Edition,Genre,GenreId")] Book book)
    {
        // Check if ISBN in URL does not match ISBN in Book data
        if (isbn != book.ISBN) return BadRequest("ISBNs do not match");

        // Turn genre ID into a Genre object
        Genre? genre = await _context.Genres.FindAsync(book.GenreId);

        // Check if genre does not exist and display error
        if (genre == null) ModelState.AddModelError("GenreId", "Genre doesn't exist.");

        // Assign genre to book
        //book.Genre = genre;

        // Check model is valid before update
        if (ModelState.IsValid)
        {
            try
            {
                // Try to update DB
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // DB exception on update - check if book does not exist
                if (!BookExists(book.ISBN))
                {
                    return NotFound();
                }
                else
                {
                    // Not sure what the problem is, rethrow the exception (someone else's problem...)
                    throw;
                }
            }

            // Redirect user back to Index listing
            return RedirectToAction(nameof(Index));
        }

        // Get a list of genres from the DB
        ViewData["GenreList"] = new SelectList(_context.Genres, "Id", "Name");

        // Display form with book data (and error messages)
        return View(book);
    }

    // GET: Books/Delete/5
    public async Task<IActionResult> Delete(string? isbn)
    {
        // No ID, no result
        if (isbn == null) return BadRequest("Must provide an ISBN.");


        var book = await _context.Books
            .FirstOrDefaultAsync(m => m.ISBN == isbn);
        if (book == null) return NotFound("Book not found.");

        return View(book);
    }

    // POST: Books/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? isbn)
    {
        var book = await _context.Books.FindAsync(isbn);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool BookExists(string? isbn)
    {
        return _context.Books.Any(e => e.ISBN == isbn);
    }
}
