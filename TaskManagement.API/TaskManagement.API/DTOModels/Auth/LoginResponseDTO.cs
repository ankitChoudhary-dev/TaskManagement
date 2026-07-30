namespace TaskManagement.API.DTOModels.Auth
{
    /// <summary>
    /// Data transfer object representing the result of an authentication attempt.
    /// </summary>
    public class LoginResponseDTO
    {
        /// <summary>
        /// Gets or sets the display name of the authenticated user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether authentication succeeded.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets descriptive feedback or error information.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the generated JWT or session access token.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the user's system role.
        /// </summary>
        public string? Role { get; set; }
    }
}