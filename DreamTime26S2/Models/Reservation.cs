using DreamTime26S2.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamTime26S2.Models
{
    public class Reservation
    {
        [Key]
        [DisplayName("Reservation No")]
        public required int Id { get; set; }

        [StringLength(450)]
        [DisplayName("User")]
        public string? UserId { get; set; }

        [Required]
        [DisplayName("Slot")]
        public required int SlotId { get; set; }

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
        [DisplayName("Room Type")]
        public required int RoomTypeId { get; set; }

        [Required]
        [Range(1, 500)]
        [DisplayName("Number of People")]
        public required ushort NumberOfPeople { get; set; } = 1;

        [Required]
        [DisplayName("First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be 2-50 characters")]
        public required string FirstName { get; set; } = string.Empty;

        [Required]
        [DisplayName("Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be 2-50 characters")]
        public required string LastName { get; set; } = string.Empty;

        [NotMapped]
        [DisplayName("Full Name")]
        public string FullName { get => $"{FirstName?.Trim()} {LastName?.Trim()}"; }

        [StringLength(256)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public string? Notes { get; set; }

        public ReservationStatus Status { get; set; }

        public enum ReservationStatus
        {
            Pending = 0,
            Confirmed = 1,
            InProgress = 2,
            Completed = 3,
            Cancelled = 4,
        }


        // Assocations (Navigation Properties)

        [DeleteBehavior(DeleteBehavior.SetNull)]
        public ApplicationUser? User { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Slot? Slot { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        [DisplayName("Start Time")]
        public Timeslot? StartTimeslot { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        [DisplayName("End Time")]
        public Timeslot? EndTimeslot { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public RoomType? RoomType { get; set; }


        // Define a many-to-many relationship between Reservation and Room (check the Room model)
        [DisplayName("Assigned Rooms")]
        public ICollection<Room> Rooms { get; } = [];
    }
}
