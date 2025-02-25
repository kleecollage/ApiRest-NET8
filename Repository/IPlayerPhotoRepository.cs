namespace ApiRest.Repository;

public interface IPlayerPhotoRepository<T>
{
  T GetById(int id);
  List<T> GetPhotosByPlayer(int Id);
  void Add(T entity);
  void Delete(int id);
}