using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;

namespace API.Data
{
  public class Seed
  {
    public static async Task SeedUsers(DataContext context)
    {
      // use "Any" to check if we have users already. if true, no need to seed. return
      if (await context.Users.AnyAsync()) return;

      var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
      var users = JsonSerializer.Deserialize<List<Appuser>>(userData);
      foreach (var user in users)
      {
        using var hmac = new HMACSHA512();

        user.UserName = user.UserName.ToLower();
        user.Passwordhash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
        user.PasswordSalt = hmac.Key;

        context.Users.Add(user);
      }

      await context.SaveChangesAsync();
    }
  }
}