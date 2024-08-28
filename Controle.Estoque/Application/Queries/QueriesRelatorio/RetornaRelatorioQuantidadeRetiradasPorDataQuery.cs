using Controle.Estoque.Application.DTOs;
using Controle.Estoque.Data.Repository.Dapper;
using MediatR;

namespace Controle.Estoque.Application.Queries.QueriesRelatorio
{
    public class RetornaRelatorioQuantidadeRetiradasPorDataQuery : IRequest<IEnumerable<RelatorioDTO>>
    {
        public class RetornaRelatorioQuantidadeRetiradasPorDataQueryHandler : IRequestHandler<RetornaRelatorioQuantidadeRetiradasPorDataQuery, IEnumerable<RelatorioDTO>>
        {
            private readonly IRelatorioDapperRepository _relatorioDapperRepository;

            public RetornaRelatorioQuantidadeRetiradasPorDataQueryHandler(IRelatorioDapperRepository relatorioDapperRepository)
            {
                _relatorioDapperRepository = relatorioDapperRepository;
            }

            public async Task<IEnumerable<RelatorioDTO>> Handle(RetornaRelatorioQuantidadeRetiradasPorDataQuery request, CancellationToken cancellationToken)
            {
                var relatorio = await _relatorioDapperRepository.RetornaRelatorioQuantidadeRetiradasPorData();
                return relatorio;
            }
        }
    }
}