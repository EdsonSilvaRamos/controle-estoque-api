using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository.Dapper;
using MediatR;

namespace Controle.Estoque.Application.Queries.QueriesRetiradaProduto
{
    public class RetornaRetiradaProdutoPorIdQuery : IRequest<RetiradaProduto>
    {
        public Guid Id { get; set; }

        public class RetornaRetiradaProdutoPorIdQueryHandler : IRequestHandler<RetornaRetiradaProdutoPorIdQuery, RetiradaProduto>
        {
            private readonly IDapperRepository<RetiradaProduto> _retiradaProdutoDapperRepository;

            public RetornaRetiradaProdutoPorIdQueryHandler(IDapperRepository<RetiradaProduto> retiradaProdutoDapperRepository)
            {
                _retiradaProdutoDapperRepository = retiradaProdutoDapperRepository;
            }

            public async Task<RetiradaProduto> Handle(RetornaRetiradaProdutoPorIdQuery request, CancellationToken cancellationToken)
            {
                var retiradaProduto = await _retiradaProdutoDapperRepository.GetById(request.Id);
                return retiradaProduto;
            }
        }
    }
}