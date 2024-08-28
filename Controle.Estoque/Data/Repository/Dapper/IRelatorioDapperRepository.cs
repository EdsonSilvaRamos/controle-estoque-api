using Controle.Estoque.Application.DTOs;

namespace Controle.Estoque.Data.Repository.Dapper
{
    public interface IRelatorioDapperRepository
    {
        Task<IEnumerable<RelatorioDTO>> RetornaRelatorioQuantidadeRetiradasPorData();
    }
}