using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
