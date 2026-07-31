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
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address for the new account.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the new account.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}