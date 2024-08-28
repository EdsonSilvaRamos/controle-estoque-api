using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository.Dapper;
using MediatR;

namespace Controle.Estoque.Application.Queries.QueriesProduto
{
    public class RetornaListaProdutoQuery : IRequest<IEnumerable<Produto>>
    {
        public class RetornaListaProdutoQueryHandler : IRequestHandler<RetornaListaProdutoQuery, IEnumerable<Produto>>
        {
            private readonly IDapperRepository<Produto> _produtoDapperRepository;

            public RetornaListaProdutoQueryHandler(IDapperRepository<Produto> produtoDapperRepository)
            {
                _produtoDapperRepository = produtoDapperRepository;
            }

            public async Task<IEnumerable<Produto>> Handle(RetornaListaProdutoQuery request, CancellationToken cancellationToken)
            {
                var listaProduto = await _produtoDapperRepository.GetAll();
                return listaProduto.ToList();
            }
        }
    }
}