using System.ComponentModel.DataAnnotations;

namespace DreamTime26S2.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string Name { get; set; }  // Set as "required" to force a value to be provided when creating a new Genre object
        //public string Name { get; set; } = string.Empty;  // Initialise to empty string to avoid null warnings
        //public string Name { get; set; } = null!;  // Supress null warnings using null-forgiving operator
    }
}
