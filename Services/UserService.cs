using Microsoft.EntityFrameworkCore;
using Mappy.Repos;
using Mappy.Models;

namespace Mappy.Services;

public class UserService
{
  private readonly DefaultDbContext _db;

  public UserService(DefaultDbContext dbContext)
  {
    _db = dbContext;
  }

  public async Task<bool> AddAsync(UserModel user)
  {
    await _db.Users.AddAsync(user);

    var changedEntries = await _db.SaveChangesAsync();

    return changedEntries > 0;
  }

  public async Task<UserModel?> GetAsync(string email)
  {
    return await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
  }

  public async Task<bool> ExistByEmail(string email)
  {
    return await _db.Users.AnyAsync(user => user.Email == email);
  }
}