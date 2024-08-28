using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository.Dapper;
using MediatR;

namespace Controle.Estoque.Application.Queries.QueriesProduto
{
    public class RetornaProdutoPorIdQuery : IRequest<Produto>
    {
        public Guid Id { get; set; }

        public class RetornaProdutoPorIdQueryHandler : IRequestHandler<RetornaProdutoPorIdQuery, Produto>
        {
            private readonly IDapperRepository<Produto> _produtoDapperRepository;

            public RetornaProdutoPorIdQueryHandler(IDapperRepository<Produto> produtoDapperRepository)
            {
                _produtoDapperRepository = produtoDapperRepository;
            }

            public async Task<Produto> Handle(RetornaProdutoPorIdQuery request, CancellationToken cancellationToken)
            {
                var produto = await _produtoDapperRepository.GetById(request.Id);
                return produto;
            }
        }
    }
}