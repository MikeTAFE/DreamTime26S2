using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamTime26S2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be 2-50 characters")]
        public required string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be 2-50 characters")]
        public required string LastName { get; set; } = string.Empty;

        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }
        //public string FullName { get => FirstName + " " + LastName; }
    }
}
