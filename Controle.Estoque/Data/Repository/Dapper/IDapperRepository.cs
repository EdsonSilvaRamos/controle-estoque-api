namespace Controle.Estoque.Data.Repository.Dapper
{
    public interface IDapperRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);
    }
}