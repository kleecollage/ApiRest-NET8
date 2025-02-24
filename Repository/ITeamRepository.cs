namespace ApiRest.Repository;

public interface ITeamRepository<T>
{
  T GetById(int id);
  IEnumerable<T> GetAll();
  void Add(T entity);
  void Update(T entity);
  void Delete(int id);
}