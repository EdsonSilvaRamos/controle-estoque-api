using Controle.Estoque.Application.DTOs;
using Dapper;
using System.Data;

namespace Controle.Estoque.Data.Repository.Dapper
{
    public class RelatorioDapperRepository : IRelatorioDapperRepository
    {
        private readonly IDbConnection _dbConnection;

        public RelatorioDapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<RelatorioDTO>> RetornaRelatorioQuantidadeRetiradasPorData()
        {
            string query = @"SELECT p.Descricao AS DescricaoProduto,
	                                p.PartNumber,
                                    SUM(rp.Quantidade) AS QuantidadePecasRetiradas,
                                    AVG(rp.ValorCusto) AS ValorCusto,
                                    rp.DataCadastro AS DataRetiradaProduto
                               FROM produtos p
                              INNER JOIN retiradasproduto rp ON rp.IdProduto = p.Id
                              GROUP BY p.Descricao, p.PartNumber, rp.DataCadastro;";

            var relatorio = await _dbConnection.QueryAsync<RelatorioDTO>(query);
            return relatorio;
        }
    }
}