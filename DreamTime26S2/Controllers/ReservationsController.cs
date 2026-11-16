using DreamTime26S2.Data;
using DreamTime26S2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class ReservationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReservationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Reservations
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Reservations.ToListAsync());
    }

    // GET: Reservations/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = await _context.Reservations
            .FirstOrDefaultAsync(m => m.Id == id);
        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }

    // GET: Reservations/Create
    public IActionResult Create()
    {
        PopulateViewData();
        return View();
    }

    // POST: Reservations/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,UserId,SlotId,StartTime,EndTime,RoomTypeId,NumberOfPeople,FirstName,LastName,FullName,Email,Phone,Notes,User,Slot,StartTimeslot,EndTimeslot,RoomType,Rooms")] Reservation reservation)
    {
        if (ModelState.IsValid)
        {
            _context.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        PopulateViewData(reservation);
        return View(reservation);
    }

    // GET: Reservations/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }

        PopulateViewData(reservation);
        return View(reservation);
    }

    // POST: Reservations/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,UserId,SlotId,StartTime,EndTime,RoomTypeId,NumberOfPeople,FirstName,LastName,FullName,Email,Phone,Notes,User,Slot,StartTimeslot,EndTimeslot,RoomType,Rooms")] Reservation reservation)
    {
        if (id != reservation.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(reservation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(reservation.Id))
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

        PopulateViewData(reservation);
        return View(reservation);
    }

    // GET: Reservations/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = await _context.Reservations
            .FirstOrDefaultAsync(m => m.Id == id);
        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }

    // POST: Reservations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation != null)
        {
            _context.Reservations.Remove(reservation);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ReservationExists(int? id)
    {
        return _context.Reservations.Any(e => e.Id == id);
    }


    /// <summary>
    /// Populates the ViewData object with data for the dropdown lists
    /// </summary>
    /// <param name="startTime">Default value for the start time</param>
    /// <param name="endTime">Default value for the end time</param>
    /// <param name="roomTypeId">Default value for the room type</param>
    /// <param name="slotId">Default value for the slot</param>
    /// <param name="userId">Default value for the user</param>
    private void PopulateViewData(TimeOnly? startTime, TimeOnly? endTime, int? roomTypeId, int? slotId, string? userId)
    {
        // TODO: Add OrderBy() to the lists
        ViewData["StartTimeList"] = new SelectList(_context.Timeslots, "Time", "Time", startTime);
        ViewData["EndTimeList"] = new SelectList(_context.Timeslots, "Time", "Time", endTime);
        ViewData["RoomTypeList"] = new SelectList(_context.RoomTypes, "Id", "Name", roomTypeId);

        // Get a list of slots fomatted with DATE - SLOTTYPE (STARTTIME-ENDTIME)
        //ViewData["SlotList"] = new SelectList(_context.Slots.Include(s => s.SlotType).Select(s => new
        //{
        //    Id = s.Id,
        //    DisplayValue = $"{s.Date} - {s.SlotType.Name} ({s.StartTime}-{s.EndTime})",
        //}), "Id", "DisplayValue", slotId);

        // Get a list of slots using Slot.Description property
        ViewData["SlotList"] = new SelectList(_context.Slots.Include(s => s.SlotType), "Id", "Description", slotId);

        // Get an ordered list of users displaying full names
        ViewData["UserList"] = new SelectList(_context.Users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName), "Id", "FullName", userId);
    }

    /// <param name="reservation">The Reservation object to use for default values</param>
    /// <inheritdoc cref="PopulateViewData(TimeOnly? startTime, TimeOnly? endTime, int? roomTypeId, int? slotId, string? userId)" />
    private void PopulateViewData(Reservation reservation)
    {
        PopulateViewData(reservation.StartTime, reservation.EndTime, reservation.RoomTypeId, reservation.SlotId, reservation.UserId);
    }

    /// <inheritdoc cref="PopulateViewData(TimeOnly? startTime, TimeOnly? endTime, int? roomTypeId, int? slotId, string? userId)" />
    private void PopulateViewData()
    {
        PopulateViewData(null, null, null, null, null);
    }
}
