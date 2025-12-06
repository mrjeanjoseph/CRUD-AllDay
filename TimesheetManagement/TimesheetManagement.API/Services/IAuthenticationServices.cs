namespace TimesheetManagement.API.Services;

public interface IJwtTokenService
{
    string GenerateToken(Guid userId, string email, string role);
    bool ValidateToken(string token);
    Guid? GetUserIdFromToken(string token);
}

public interface IPasswordHashingService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}