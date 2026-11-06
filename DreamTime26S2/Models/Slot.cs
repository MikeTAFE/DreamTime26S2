using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamTime26S2.Models
{
    // Composite primary key of Date + SlotTypeId
    //[PrimaryKey(nameof(Date), nameof(SlotTypeId))]
    public class Slot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        public required DateOnly Date { get; set; }

        [Required]
        [DisplayName("Slot Type")]
        public required int SlotTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(StartTimeslot))]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        [DisplayName("Start Time")]
        public required TimeOnly StartTime { get; set; }

        [Required]
        [ForeignKey(nameof(EndTimeslot))]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        [DisplayName("End Time")]
        public required TimeOnly EndTime { get; set; }

        [Required]
        public required bool Available { get; set; } = true;


        // Assocations (Navigation Properties)

        [DeleteBehavior(DeleteBehavior.Restrict)]
        [DisplayName("Slot Type")]
        public SlotType? SlotType { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        [DisplayName("Start Time")]
        public Timeslot? StartTimeslot { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        [DisplayName("End Time")]
        public Timeslot? EndTimeslot { get; set; }


        public Slot()
        {
            Date = DateOnly.FromDateTime(DateTime.Today);
            SlotTypeId = 1;                     // Morning (hard-coded ID)
            SlotTypeId = 0;                     // int default to 0, equivalent of "null"
            StartTime = new TimeOnly(9, 00);    // 9am
            EndTime = new TimeOnly(12, 00);     // 12pm
            Available = true;
        }

        public Slot(int slotTypeId, DateOnly date, TimeOnly startTime, TimeOnly endTime, bool available = true)
        {
            Date = date;
            SlotTypeId = slotTypeId;
            StartTime = startTime;
            EndTime = endTime;
            Available = available;
        }
    }
}
