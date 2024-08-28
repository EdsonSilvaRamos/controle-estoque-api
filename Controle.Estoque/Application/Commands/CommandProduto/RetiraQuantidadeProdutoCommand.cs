using Controle.Estoque.Application.Commands.CommandLogErro;
using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository;
using MediatR;

namespace Controle.Estoque.Application.Commands.CommandProduto
{
    public class RetiraQuantidadeProdutoCommand : IRequest<Produto>
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }

        public class RetiraQuantidadeProdutoCommandHandler : IRequestHandler<RetiraQuantidadeProdutoCommand, Produto>
        {
            private readonly IMediator _mediator;
            private readonly IRepository<Produto> _repository;

            public RetiraQuantidadeProdutoCommandHandler(IMediator mediator, IRepository<Produto> repository)
            {
                _mediator = mediator;
                _repository = repository;
            }

            public async Task<Produto> Handle(RetiraQuantidadeProdutoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var produtoEncontrado = await _repository.GetById(request.Id);
                    if (produtoEncontrado == null)
                        throw new ApplicationException("Produto não encontrado");

                    produtoEncontrado.RetiraQuantidade(request.Quantidade, dataAtualizacao: DateTime.Now);

                    if (produtoEncontrado.Quantidade < 0)
                        throw new ApplicationException("Quantidade a ser retirada maior que o permitido");

                    _repository.Edit(produtoEncontrado);
                    await _repository.SaveChanges();

                    return produtoEncontrado;
                }
                catch (Exception ex)
                {
                    var messagemErro = $"Erro ao retirar quantidade de produto cod: {request.Id}. Exceção: {ex.Message}";
                    var logErroCommand = new CriacaoLogErroCommand { DescricaoErro = messagemErro };
                    await _mediator.Send(logErroCommand);
                    throw new ApplicationException(messagemErro);
                }
            }
        }
    }
}