using Controle.Estoque.Application.Commands.CommandLogErro;
using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository;
using MediatR;

namespace Controle.Estoque.Application.Commands.CommandProduto
{
    public class AtualizaProdutoCommand : IRequest<Produto>
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string PartNumber { get; set; }
        public decimal ValorCusto { get; set; }

        public class AtualizaProdutoCommandHandler : IRequestHandler<AtualizaProdutoCommand, Produto>
        {
            private readonly IMediator _mediator;
            private readonly IRepository<Produto> _repository;

            public AtualizaProdutoCommandHandler(IMediator mediator, IRepository<Produto> repository)
            {
                _mediator = mediator;
                _repository = repository;
            }

            public async Task<Produto> Handle(AtualizaProdutoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var produtoEncontrado = await _repository.GetById(request.Id);
                    if (produtoEncontrado == null)
                        throw new ApplicationException("Produto não encontrado");

                    produtoEncontrado.Atualiza(descricao: request.Descricao, partNumber: request.PartNumber, valorCusto: request.ValorCusto, dataAtualizacao: DateTime.Now);

                    _repository.Edit(produtoEncontrado);
                    await _repository.SaveChanges();

                    return produtoEncontrado;
                }
                catch (Exception ex)
                {
                    var messagemErro = $"Erro ao atualizar produto cod: {request.Id}. Exceção: {ex.Message}";
                    var logErroCommand = new CriacaoLogErroCommand { DescricaoErro = messagemErro };
                    await _mediator.Send(logErroCommand);
                    throw new ApplicationException(messagemErro);
                }
            }
        }
    }
}