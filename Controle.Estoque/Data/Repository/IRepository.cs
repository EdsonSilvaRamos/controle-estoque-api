namespace Controle.Estoque.Data.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);

        Task Add(T item);

        void Edit(T item);

        void Delete(T item);

        Task SaveChanges();
    }
}