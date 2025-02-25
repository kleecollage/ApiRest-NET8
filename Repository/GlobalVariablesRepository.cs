using ApiRest.Data;
using ApiRest.Models;

namespace ApiRest.Repository;

public class GlobalVariablesRepository(ApplicationDbContext context): IGlobalVariablesRepository<VariableGlobal>
{
  private readonly ApplicationDbContext _context = context;

    public VariableGlobal GetById(int Id)
    {
        return _context.Set<VariableGlobal>().Find(Id);
    }
}