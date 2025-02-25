namespace ApiRest.Repository;

public interface IGlobalVariablesRepository<T> {
  T GetById(int Id);
}