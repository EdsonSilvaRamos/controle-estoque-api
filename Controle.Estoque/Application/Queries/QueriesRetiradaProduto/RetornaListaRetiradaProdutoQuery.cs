using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository.Dapper;
using MediatR;

namespace Controle.Estoque.Application.Queries.QueriesRetiradaProduto
{
    public class RetornaListaRetiradaProdutoQuery : IRequest<IEnumerable<RetiradaProduto>>
    {
        public class RetornaListaRetiradaProdutoQueryHandler : IRequestHandler<RetornaListaRetiradaProdutoQuery, IEnumerable<RetiradaProduto>>
        {
            private readonly IDapperRepository<RetiradaProduto> _retiradaProdutoDapperRepository;

            public RetornaListaRetiradaProdutoQueryHandler(IDapperRepository<RetiradaProduto> retiradaProdutoDapperRepository)
            {
                _retiradaProdutoDapperRepository = retiradaProdutoDapperRepository;
            }

            public async Task<IEnumerable<RetiradaProduto>> Handle(RetornaListaRetiradaProdutoQuery request, CancellationToken cancellationToken)
            {
                var listaRetiradaProduto = await _retiradaProdutoDapperRepository.GetAll();
                return listaRetiradaProduto.ToList();
            }
        }
    }
}