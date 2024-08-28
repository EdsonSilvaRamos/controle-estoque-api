using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository.Dapper;
using MediatR;

namespace Controle.Estoque.Application.Queries.QueriesLogErro
{
    public class RetornaListaLogErroQuery : IRequest<IEnumerable<LogErro>>
    {
        public class RetornaListaLogErroQueryHandler : IRequestHandler<RetornaListaLogErroQuery, IEnumerable<LogErro>>
        {
            private readonly IDapperRepository<LogErro> _logErroDapperRepository;

            public RetornaListaLogErroQueryHandler(IDapperRepository<LogErro> logErroDapperRepository)
            {
                _logErroDapperRepository = logErroDapperRepository;
            }

            public async Task<IEnumerable<LogErro>> Handle(RetornaListaLogErroQuery request, CancellationToken cancellationToken)
            {
                var listaLogErro = await _logErroDapperRepository.GetAll();
                return listaLogErro.ToList();
            }
        }
    }
}