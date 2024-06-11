using Mappy.Models;
using Mappy.Repos;
using Microsoft.EntityFrameworkCore;

namespace Mappy.Services;

public class AccidentService
{
	private readonly DefaultDbContext _db;

	public AccidentService(DefaultDbContext dbContext)
	{
		_db = dbContext;
	}

	public async Task<AccidentModel> AddAsync(AccidentModel accident)
	{
		var result = await _db.Accidents.AddAsync(accident);

		await _db.SaveChangesAsync();

		return result.Entity;
	}

	public async Task<List<AccidentModel>> GetAllAsync()
	{
		return await _db.Accidents.ToListAsync();
	}
}