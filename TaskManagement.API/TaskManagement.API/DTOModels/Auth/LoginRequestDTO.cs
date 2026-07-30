using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.DTOModels.Auth
{
    /// <summary>
    /// Data transfer object containing the required credentials for user authentication.
    /// </summary>
    public class LoginRequestDTO
    {
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's account password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
    }
}