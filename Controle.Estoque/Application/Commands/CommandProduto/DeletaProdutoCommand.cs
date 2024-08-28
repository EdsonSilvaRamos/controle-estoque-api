using Controle.Estoque.Application.Commands.CommandLogErro;
using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository;
using MediatR;

namespace Controle.Estoque.Application.Commands.CommandProduto
{
    public class DeletaProdutoCommand : IRequest<Produto>
    {
        public Guid Id { get; set; }

        public class DeletaProdutoCommandHandler : IRequestHandler<DeletaProdutoCommand, Produto>
        {
            private readonly IMediator _mediator;
            private readonly IRepository<Produto> _repository;

            public DeletaProdutoCommandHandler(IMediator mediator, IRepository<Produto> repository)
            {
                _mediator = mediator;
                _repository = repository;
            }

            public async Task<Produto> Handle(DeletaProdutoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var produtoEncontrado = await _repository.GetById(request.Id);
                    if (produtoEncontrado == null)
                        throw new ApplicationException("Produto não encontrado");                    

                    _repository.Delete(produtoEncontrado);
                    await _repository.SaveChanges();

                    return produtoEncontrado;
                }
                catch (Exception ex)
                {
                    var mensagemErro = $"Erro ao excluir produto cod: {request.Id}. Exceção: {ex.Message}";
                    var logErroCommand = new CriacaoLogErroCommand { DescricaoErro = mensagemErro };
                    await _mediator.Send(logErroCommand);
                    throw new ApplicationException(mensagemErro);
                }
            }
        }
    }
}