using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DreamTime26S2.Models
{
    public class Book
    {
        // TODO: Add regex (10 or 13 digits) to ISBN validation
        [Key]
        [MaxLength(13)]
        public required string ISBN { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string Title { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string Author { get; set; }

        [Required]
        [Range(1, 9999)]
        public int Pages { get; set; }

        [Required]
        public required string Description { get; set; }

        [StringLength(100)]
        [DataType(DataType.ImageUrl)]
        public string? ImageFilename { get; set; }

        [Required]
        [Range(0, 9999)]
        public short Copies { get; set; }

        [StringLength(100)]
        public string? Publisher { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly DatePublished { get; set; }

        [StringLength(20)]
        public string? Edition { get; set; }


        // Links/relationships/associations to other entities
        [Required]
        [DeleteBehavior(DeleteBehavior.Restrict)]  // Stops cascade delete action - can also use NoAction (same in SQL Server)
        public required Genre Genre { get; set; }

        public int GenreId { get; set; }  // Foreign key to Genre - if not specifed, EF will automatically create a field/column

    }
}