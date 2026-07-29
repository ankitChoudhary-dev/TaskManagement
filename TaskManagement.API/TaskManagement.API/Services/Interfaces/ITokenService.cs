namespace TaskManagement.API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string email, string role);
    }
}