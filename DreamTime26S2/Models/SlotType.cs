using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DreamTime26S2.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class SlotType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Must be 2-20 characters")]
        public required string Name { get; set; } = string.Empty;
    }
}
