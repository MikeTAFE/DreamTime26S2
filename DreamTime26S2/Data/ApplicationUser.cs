using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamTime26S2.Data
{
    public class ApplicationUser : IdentityUser
    {
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
        public string FullName { get => $"{FirstName} {LastName}"; }
        //public string FullName { get => FirstName + " " + LastName; }
    }
}
