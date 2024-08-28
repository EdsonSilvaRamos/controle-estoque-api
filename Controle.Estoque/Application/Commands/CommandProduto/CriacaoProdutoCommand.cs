using Controle.Estoque.Application.Commands.CommandLogErro;
using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository;
using MediatR;

namespace Controle.Estoque.Application.Commands.CommandProduto
{
    public class CriacaoProdutoCommand : IRequest<Produto>
    {
        public string Descricao { get; set; }
        public string PartNumber { get; set; }
        public decimal ValorCusto { get; set; }
        public int Quantidade { get; set; }

        public class CriacaoProdutoCommandHandler : IRequestHandler<CriacaoProdutoCommand, Produto>
        {
            private readonly IMediator _mediator;
            private readonly IRepository<Produto> _repository;

            public CriacaoProdutoCommandHandler(IMediator mediator, IRepository<Produto> repository)
            {
                _mediator = mediator;
                _repository = repository;
            }

            public async Task<Produto> Handle(CriacaoProdutoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var novoProduto = new Produto { Descricao = request.Descricao, PartNumber = request.PartNumber, Quantidade = request.Quantidade, ValorCusto = request.ValorCusto, DataCadastro = DateTime.Today };

                    await _repository.Add(novoProduto);
                    await _repository.SaveChanges();

                    return novoProduto;
                }
                catch (Exception ex)
                {
                    var logErroCommand = new CriacaoLogErroCommand { DescricaoErro = $"Erro ao cadastrar produto. Exceção: {ex.Message}" };
                    await _mediator.Send(logErroCommand);
                    return null;
                }
            }
        }
    }
}