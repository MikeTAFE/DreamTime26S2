using System.ComponentModel.DataAnnotations;

namespace DreamTime26S2.Models
{
    public class Timeslot
    {
        [Key]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public TimeOnly Time { get; set; }

        // Constructors
        public Timeslot() { }

        public Timeslot(TimeOnly time)
        {
            Time = time;
        }
    }
}
