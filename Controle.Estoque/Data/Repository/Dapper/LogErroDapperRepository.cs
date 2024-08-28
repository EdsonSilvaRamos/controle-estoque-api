using Controle.Estoque.Application.Models;
using Dapper;
using System.Data;

namespace Controle.Estoque.Data.Repository.Dapper
{
    public class LogErroDapperRepository : IDapperRepository<LogErro>
    {
        private readonly IDbConnection _dbConnection;

        public LogErroDapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<LogErro> GetById(Guid id)
        {
            string query = "SELECT * FROM logserro WHERE Id = @Id";
            var logErro = await _dbConnection.QuerySingleAsync<LogErro>(query, new { Id = id });
            return logErro;
        }

        public async Task<IEnumerable<LogErro>> GetAll()
        {
            string query = "SELECT * FROM logserro";
            var listaLogErro = await _dbConnection.QueryAsync<LogErro>(query);
            return listaLogErro;
        }
    }
}