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
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's account password.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}