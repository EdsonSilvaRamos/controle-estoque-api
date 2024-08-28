using Controle.Estoque.Application.Models;
using Dapper;
using System.Data;

namespace Controle.Estoque.Data.Repository.Dapper
{
    public class ProdutoDapperRepository : IDapperRepository<Produto>
    {
        private readonly IDbConnection _dbConnection;

        public ProdutoDapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Produto> GetById(Guid id)
        {
            string query = "SELECT * FROM produtos WHERE Id = @Id";
            var produto = await _dbConnection.QuerySingleAsync<Produto>(query, new { Id = id });
            return produto;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            string query = "SELECT * FROM produtos";
            var listaProduto = await _dbConnection.QueryAsync<Produto>(query);
            return listaProduto;
        }
    }
}