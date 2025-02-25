using ApiRest.Data;
using ApiRest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Repository;

public class UserRepository(ApplicationDbContext context): IUserRepository<Usuario>
{
  private readonly ApplicationDbContext _context = context;

    public void Add(Usuario entity)
    {
        _context.Set<Usuario>().Add(entity);
        _context.SaveChanges();
    }

    public async Task<Usuario> GetUser(string email, string password)
    {
        return await _context.Set<Usuario>()
          .Where(u => u.Correo == email)
          .Where(u => u.Password == password)
          .FirstOrDefaultAsync();
    }

    public async Task<Usuario> GetUserByEmail(string email)
    {
        return await _context.Set<Usuario>()
          .Where(u => u.Correo == email)
          .FirstOrDefaultAsync();
    }

    public async Task<Usuario> GetUserByEmailActive(string email)
    {
        return await _context.Set<Usuario>()
          .Where(u => u.Correo == email)
          .Where(u => u.Estado == 1 )
          .FirstOrDefaultAsync();
    }

    public async Task<Usuario> GetUserByToken(string token)
    {
        return await _context.Set<Usuario>()
          .Where(u => u.Token == token)
          .FirstOrDefaultAsync();
    }

    public async Task<Usuario> GetUserByTokenActive(string token)
    {
      return await _context.Set<Usuario>()
          .Where(u => u.Token == token)
          .Where(u => u.Estado == 1)
          .FirstOrDefaultAsync();
    }

    public void Update(Usuario entity)
    {
      _context.Entry(entity).State = EntityState.Modified;
      _context.SaveChanges();
    }
}