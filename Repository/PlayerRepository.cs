using ApiRest.Data;
using ApiRest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Repository;

public class PlayerRepository(ApplicationDbContext context): IPlayerRepository<Jugador>
{
  private readonly ApplicationDbContext _context = context;

    public void Add(Jugador entity)
    {
        _context.Set<Jugador>().Add(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
      var player = _context.Set<Jugador>().Find(id);
      if (player != null)
      {
        _context.Set<Jugador>().Remove(player);
        _context.SaveChanges();
      }
    }

    public IEnumerable<Jugador> GetAll()
    {
      return [.. _context.Set<Jugador>()
        .Include(p => p.Equipo)
        .OrderByDescending(p => p.Id)
      ];
    }

    public Jugador GetById(int id)
    {
      return _context.Set<Jugador>()
        .Where(p => p.Id == id)
        .Include(p => p.Equipo)
        .First();
    }

    public void Update(Jugador entity)
    {
      _context.Entry(entity).State = EntityState.Modified;
      _context.SaveChanges();
    }
}