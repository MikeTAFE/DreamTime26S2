using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DreamTime26S2.Models
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "I require a name!")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Name must be between 2-80 characters")]
        public required string Name { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Capacity must be between 0-100")]
        public required ushort Capacity { get; set; }

        public RoomType()
        {
            Name = string.Empty;
            Capacity = 0;
        }

        public RoomType(string name, ushort capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        // new RoomType()
        // new RoomType("Test Room", 45)
        // new RoomType { Name = "Test Room", Capacity = 45 }

        // Associations (navigation properties)
        public ICollection<Room>? Rooms { get; set; }
    }
}
