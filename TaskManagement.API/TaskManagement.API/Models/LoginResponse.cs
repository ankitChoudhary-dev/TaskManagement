public class LoginResponse
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? Token { get; set; }

    public string? Role { get; set; }
}