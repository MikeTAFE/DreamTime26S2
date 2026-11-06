using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DreamTime26S2.Models;
using DreamTime26S2.Data;

public class SlotsController : Controller
{
    private readonly ApplicationDbContext _context;

    public SlotsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Slots
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Slots.ToListAsync());
    }

    // GET: Slots/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var slot = await _context.Slots
            .FirstOrDefaultAsync(m => m.Id == id);
        if (slot == null)
        {
            return NotFound();
        }

        return View(slot);
    }

    // GET: Slots/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Slots/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Date,SlotTypeId,StartTime,EndTime,Available,SlotType,StartTimeslot,EndTimeslot")] Slot slot)
    {
        if (ModelState.IsValid)
        {
            _context.Add(slot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(slot);
    }

    // GET: Slots/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var slot = await _context.Slots.FindAsync(id);
        if (slot == null)
        {
            return NotFound();
        }
        return View(slot);
    }

    // POST: Slots/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Date,SlotTypeId,StartTime,EndTime,Available,SlotType,StartTimeslot,EndTimeslot")] Slot slot)
    {
        if (id != slot.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(slot);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotExists(slot.Id))
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
        return View(slot);
    }

    // GET: Slots/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var slot = await _context.Slots
            .FirstOrDefaultAsync(m => m.Id == id);
        if (slot == null)
        {
            return NotFound();
        }

        return View(slot);
    }

    // POST: Slots/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var slot = await _context.Slots.FindAsync(id);
        if (slot != null)
        {
            _context.Slots.Remove(slot);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SlotExists(int? id)
    {
        return _context.Slots.Any(e => e.Id == id);
    }
}
