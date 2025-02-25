namespace ApiRest.Repository;

public interface IUserRepository<T>
{
  Task<T> GetUser(string email, string password);
  Task<T> GetUserByEmail(string email);
  Task<T> GetUserByEmailActive(string email);
  Task<T> GetUserByToken(string token);
  Task<T> GetUserByTokenActive(string token);
  void Add(T entity);
  void Update(T entity);
}