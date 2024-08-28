using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository.Dapper;
using MediatR;

namespace Controle.Estoque.Application.Queries.QueriesLogErro
{
    public class RetornaLogErroPorIdQuery : IRequest<LogErro>
    {
        public Guid Id { get; set; }

        public class RetornaLogErroPorIdQueryHandler : IRequestHandler<RetornaLogErroPorIdQuery, LogErro>
        {
            private readonly IDapperRepository<LogErro> _logErroDapperRepository;

            public RetornaLogErroPorIdQueryHandler(IDapperRepository<LogErro> logErroDapperRepository)
            {
                _logErroDapperRepository = logErroDapperRepository;
            }

            public async Task<LogErro> Handle(RetornaLogErroPorIdQuery request, CancellationToken cancellationToken)
            {
                var logErro = await _logErroDapperRepository.GetById(request.Id);
                return logErro;
            }
        }
    }
}