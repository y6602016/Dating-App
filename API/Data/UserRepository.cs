using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    public UserRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Appuser>> GetUserAsync()
    {
      return await _context.Users
        .Include(p => p.Photos) // eager loading to retrieve related data (but circular reference issue)
        .ToListAsync();
    }

    public async Task<Appuser> GetUserByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public async Task<Appuser> GetUserByUsernameAsync(string username)
    {
      return await _context.Users
        .Include(p => p.Photos) // eager loading to retrieve related data (but circular reference issue)
        .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<bool> SaveAllAsync()
    {
      // return the num of data that save changes, it should be > 0
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(Appuser user)
    {
      // set the user data state = modified, add it into track list
      _context.Entry(user).State = EntityState.Modified;
    }
  }
}