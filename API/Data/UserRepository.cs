using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public UserRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<MemberDto> GetMemberAsync(string username)
    {
      return await _context.Users
        .Where(x => x.UserName == username)
        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
      return await _context.Users
        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        .ToListAsync();
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
        .Where(u => u.UserName == username)
        .Include(p => p.Photos) // eager loading to retrieve related data (but circular reference issue)
        .SingleOrDefaultAsync();
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