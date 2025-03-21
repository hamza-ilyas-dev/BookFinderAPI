using BookFinderAPI.Models;

namespace BookFinderAPI.Interfaces
{
    public interface IGenerateJwtToken
    {
        string GenerateJwtTokens(User user);
    }
}
