namespace TaskManagement.API.Services.Interfaces
{
    /// <summary>
    /// Defines service contract for generating JWT tokens for user authentication and authorization.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a signed JWT access token containing specified user identity claims.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="email">The email address of the user.</param>
        /// <param name="role">The system role assigned to the user.</param>
        /// <returns>A signed JWT token string.</returns>
        string GenerateToken(int userId, string email, string role);
    }
}