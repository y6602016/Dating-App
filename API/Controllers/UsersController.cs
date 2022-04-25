using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [Authorize]
  public class UsersController : BaseApiController
  {
    private readonly IUserRepository _userRepository;
    public UsersController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<Appuser>>> GetUsers()
    {

      return Ok(await _userRepository.GetUserAsync());
    }


    [HttpGet("{username}")]
    public async Task<ActionResult<Appuser>> GetUser(string username)
    {
      return await _userRepository.GetUserByUsernameAsync(username);
    }
  }
}