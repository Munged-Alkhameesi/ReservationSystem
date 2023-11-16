using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DatabaseReservation.Data
{
    public class ApplicationUser : IdentityUser

    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        // To store the name of the profile picture file
        public string ProfilePic { get; set; } = "Default.png";

    }
}
