using ApiRest.Data;
using ApiRest.Models;

namespace ApiRest.Repository;

public class PlayerPhotoRepository(ApplicationDbContext context): IPlayerPhotoRepository<JugadorFoto>
{
  private readonly ApplicationDbContext _context = context;

    public void Add(JugadorFoto entity)
    {
        _context.Set<JugadorFoto>().Add(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var photo = _context.Set<JugadorFoto>().Find(id);
        if (photo != null)
        {
          _context.Set<JugadorFoto>().Remove(photo);
          _context.SaveChanges();
        }

    }

    public JugadorFoto GetById(int id)
    {
        return _context.Set<JugadorFoto>().Find(id);
    }

    public List<JugadorFoto> GetPhotosByPlayer(int Id)
    {
        return [.. _context
          .Set<JugadorFoto>()
          .Where(p => p.JugadorId == Id)
          .OrderByDescending(x => x.Id)
        ];
    }
}