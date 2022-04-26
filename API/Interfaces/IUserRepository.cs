using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
  public interface IUserRepository
  {
    // update works add the user to track, not querying data from db, no need to be async
    void Update(Appuser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<Appuser>> GetUserAsync();
    Task<Appuser> GetUserByIdAsync(int id);
    Task<Appuser> GetUserByUsernameAsync(string username);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto> GetMemberAsync(string username);
  }
}