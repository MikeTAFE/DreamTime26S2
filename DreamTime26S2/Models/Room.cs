using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DreamTime26S2.Models
{
    // Example: Composite primary key of Code + LibraryId
    //[PrimaryKey(nameof(Code), nameof(LibraryId))]
    public class Room
    {
        [Key]
        [StringLength(5, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]{1,2}\d{1,3}$", ErrorMessage = "Code must follow the pattern XX000, e.g. M1, AB002")]
        public required string Code { get; set; }

        // Example: if we had multiple libraries, we would need a composite primary key
        //public required int LibraryId { get; set; }

        [Required]
        [DisplayName("Room Type")]
        public required int RoomTypeId { get; set; }

        // Associations (navigation properties)
        [DeleteBehavior(DeleteBehavior.Restrict)]  // Stop cascade delete
        public RoomType? RoomType { get; set; }
    }
}
