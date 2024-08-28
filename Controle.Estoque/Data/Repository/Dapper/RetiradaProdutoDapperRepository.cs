using Controle.Estoque.Application.Models;
using Dapper;
using System.Data;

namespace Controle.Estoque.Data.Repository.Dapper
{
    public class RetiradaProdutoDapperRepository : IDapperRepository<RetiradaProduto>
    {
        private readonly IDbConnection _dbConnection;

        public RetiradaProdutoDapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<RetiradaProduto> GetById(Guid id)
        {
            string query = "SELECT * FROM retiradasproduto WHERE Id = @Id";
            var retiradaProduto = await _dbConnection.QuerySingleAsync<RetiradaProduto>(query, new { Id = id });
            return retiradaProduto;
        }

        public async Task<IEnumerable<RetiradaProduto>> GetAll()
        {
            string query = "SELECT * FROM retiradasproduto";
            var listaRetiradaProduto = await _dbConnection.QueryAsync<RetiradaProduto>(query);
            return listaRetiradaProduto;
        }
    }
}