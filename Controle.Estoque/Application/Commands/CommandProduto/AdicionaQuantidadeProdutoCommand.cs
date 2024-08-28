using Controle.Estoque.Application.Commands.CommandLogErro;
using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository;
using MediatR;

namespace Controle.Estoque.Application.Commands.CommandProduto
{
    public class AdicionaQuantidadeProdutoCommand : IRequest<Produto>
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }

        public class AdicionaQuantidadeProdutoCommandHandler : IRequestHandler<AdicionaQuantidadeProdutoCommand, Produto>
        {
            private readonly IMediator _mediator;
            private readonly IRepository<Produto> _repository;

            public AdicionaQuantidadeProdutoCommandHandler(IMediator mediator, IRepository<Produto> repository)
            {
                _mediator = mediator;
                _repository = repository;
            }

            public async Task<Produto> Handle(AdicionaQuantidadeProdutoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var produtoEncontrado = await _repository.GetById(request.Id);
                    if (produtoEncontrado == null)
                        throw new ApplicationException("Produto não encontrado");

                    produtoEncontrado.AdicionaQuantidade(request.Quantidade, dataAtualizacao: DateTime.Now);

                    _repository.Edit(produtoEncontrado);
                    await _repository.SaveChanges();

                    return produtoEncontrado;
                }
                catch (Exception ex)
                {
                    var mensagemErro = $"Erro ao adicionar quantidade de produto cod: {request.Id}. Exceção: {ex.Message}";
                    var logErroCommand = new CriacaoLogErroCommand { DescricaoErro = mensagemErro };
                    await _mediator.Send(logErroCommand);
                    throw new ApplicationException(mensagemErro);
                }
            }
        }
    }
}