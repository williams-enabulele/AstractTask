using AstractTask.Infrastruture.Identity;

namespace AstractTask.Infrastruture.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);

        int? ValidateJwtToken(string token);

        Guid GenerateRefreshToken();
    }
}