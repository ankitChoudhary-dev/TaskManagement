using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.DTOModels.Auth
{
    /// <summary>
    /// Data transfer object containing the required details for new user account registration.
    /// </summary>
    public class RegisterRequestDTO
    {
        /// <summary>
        /// Gets or sets the full name of the registering user.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address for the new account.
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the new account.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;
    }
}