using ApiRest.Data;
using ApiRest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Repository;

public class TeamRepository(ApplicationDbContext context): ITeamRepository<Equipo>
{
  private readonly ApplicationDbContext _context = context;

    public void Add(Equipo entity)
    {
        _context.Set<Equipo>().Add(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
      var team = _context.Set<Equipo>().Find(id);
      if (team != null)
      {
        _context.Set<Equipo>().Remove(team);
        _context.SaveChanges();
      }
    }

    public IEnumerable<Equipo> GetAll()
    {
        return [.. _context.Set<Equipo>().OrderByDescending(t => t.Id)];
    }

    public Equipo GetById(int id)
    {
        return _context.Set<Equipo>().Find(id);
    }

    public void Update(Equipo entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }
}