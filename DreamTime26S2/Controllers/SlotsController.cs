using DreamTime26S2.Data;
using DreamTime26S2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// Allow access to Staff OR Manager roles (default for entire controller)
[Authorize(Roles = "Staff,Manager")]
public class SlotsController : Controller
{
    private readonly ApplicationDbContext _context;

    public SlotsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Slots
    // Allow access to ANYONE (even non-logged-in users)
    //[AllowAnonymous]
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Slots.Include(s => s.SlotType).ToListAsync());
    }

    // GET: Slots/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var slot = await _context.Slots.Include(s => s.SlotType)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (slot == null)
        {
            return NotFound();
        }

        return View(slot);
    }

    // GET: Slots/Create
    // Allow access to Staff OR Manager
    //[Authorize(Roles = "Staff,Manager")]
    // Allow access to Staff AND Manager (user with BOTH roles)
    //[Authorize(Roles = "Staff")]
    //[Authorize(Roles = "Manager")]
    public IActionResult Create()
    {
        PopulateViewData();
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

        PopulateViewData(slot);
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

        PopulateViewData(slot);
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

        PopulateViewData(slot);
        return View(slot);
    }

    // GET: Slots/Delete/5
    // Only allow access to the Manager role
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var slot = await _context.Slots.Include(s => s.SlotType)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (slot == null)
        {
            return NotFound();
        }

        return View(slot);
    }

    // POST: Slots/Delete/5
    // Only allow access to the Manager role
    [Authorize(Roles = "Manager")]
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

    /// <summary>
    /// Populates the ViewData object with data for the dropdown lists
    /// </summary>
    /// <param name="slotTypeId">Default value for the slot type</param>
    /// <param name="startTime">Default value for the start time</param>
    /// <param name="endTime">Default value for the end time</param>
    private void PopulateViewData(int? slotTypeId, TimeOnly? startTime, TimeOnly? endTime)
    {
        // Sort slot types by ID (not by name)
        ViewData["SlotTypeList"] = new SelectList(_context.SlotTypes.OrderBy(st => st.Id), "Id", "Name", slotTypeId);
        ViewData["StartTimeList"] = new SelectList(_context.Timeslots, "Time", "Time", startTime);
        ViewData["EndTimeList"] = new SelectList(_context.Timeslots, "Time", "Time", endTime);
    }

    /// <param name="slot">The Slot object to use for default values</param>
    /// <inheritdoc cref="PopulateViewData(int?, TimeOnly?, TimeOnly?)" />
    private void PopulateViewData(Slot slot)
    {
        PopulateViewData(slot.SlotTypeId, slot.StartTime, slot.EndTime);
    }

    /// <inheritdoc cref="PopulateViewData(int?, TimeOnly?, TimeOnly?)" />
    private void PopulateViewData()
    {
        PopulateViewData(null, null, null);
    }
}
