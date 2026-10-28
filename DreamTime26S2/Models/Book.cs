using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamTime26S2.Models
{
    public class Book
    {
        [Key]
        [MaxLength(13)]
        [RegularExpression(@"^(\d{10}|\d{13})$", ErrorMessage = "Must be a 10- or 13-digit ISBN")]
        public required string ISBN { get; set; }

        [NotMapped]
        [DisplayName("ISBN")]
        public string ISBNFormatted { get => ISBN.Length == 10 ? ISBN : $"{ISBN.Substring(0, 3)}-{ISBN.Substring(3)}"; }

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
        [DisplayFormat(HtmlEncode = false)]
        public required string Description { get; set; }

        [StringLength(100)]
        [DataType(DataType.ImageUrl)]
        [DisplayName("Image (filename)")]
        public string? ImageFilename { get; set; }

        [Required]
        [Range(0, 9999)]
        public short Copies { get; set; }

        [StringLength(100)]
        public string? Publisher { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date Published")]
        public DateOnly DatePublished { get; set; }

        [StringLength(20)]
        public string? Edition { get; set; }


        // Links/relationships/associations to other entities
        [DeleteBehavior(DeleteBehavior.Restrict)]  // Stops cascade delete action - can also use NoAction (same in SQL Server)
        public Genre? Genre { get; set; }

        [Required]
        [DisplayName("Genre")]
        public required int GenreId { get; set; }  // Foreign key to Genre - if not specifed, EF will automatically create a field/column

    }
}