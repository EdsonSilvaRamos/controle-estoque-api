using Controle.Estoque.Application.Commands.CommandLogErro;
using Controle.Estoque.Application.Commands.CommandProduto;
using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository;
using MediatR;

namespace Controle.Estoque.Application.Commands.CommandPedidoProduto
{
    public class CriacaoRetiradaProdutoCommand : IRequest<RetiradaProduto>
    {
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }

        public class CriacaoRetiradaProdutoCommandHandler : IRequestHandler<CriacaoRetiradaProdutoCommand, RetiradaProduto>
        {
            private readonly IMediator _mediator;
            private readonly IRepository<RetiradaProduto> _repository;
            private readonly IRepository<Produto> _repositoryProduto;

            public CriacaoRetiradaProdutoCommandHandler(IMediator mediator, IRepository<RetiradaProduto> repository, IRepository<Produto> repositoryProduto)
            {
                _mediator = mediator;
                _repository = repository;
                _repositoryProduto = repositoryProduto;
            }

            public async Task<RetiradaProduto> Handle(CriacaoRetiradaProdutoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var produto = await _repositoryProduto.GetById(request.IdProduto);

                    TrataExcecoes(request, produto);

                    var retiradaProdutoCriada = new RetiradaProduto { IdProduto = request.IdProduto, Quantidade = request.Quantidade, DataCadastro = DateTime.Today, ValorCusto = produto.ValorCusto };

                    await AtualizaQuantidadeItens(request);                    

                    await _repository.Add(retiradaProdutoCriada);
                    await _repository.SaveChanges();
                    return retiradaProdutoCriada;                    
                }
                catch (Exception ex)
                {
                    var mensagemErro = $"Erro ao tentar retirar produto do estoque. Exceção: {ex.Message}";
                    var logErroCommand = new CriacaoLogErroCommand { DescricaoErro = mensagemErro };
                    await _mediator.Send(logErroCommand);
                    throw new ApplicationException(mensagemErro);
                }
            }

            private static void TrataExcecoes(CriacaoRetiradaProdutoCommand request, Produto produto)
            {   
                if (produto == null)
                {
                    throw new ApplicationException("Produto não encontrado");
                }

                if (produto.Quantidade == 0)
                {
                    throw new ApplicationException("Saldo indisponível");
                }

                if (request.Quantidade == 0)
                {
                    throw new ApplicationException("Quantidade de itens a ser retirado do estoque não informada");
                }

                if (request.Quantidade > produto.Quantidade)
                {
                    throw new ApplicationException("Não há saldo suficiente no estoque");
                }
            }

            private async Task AtualizaQuantidadeItens(CriacaoRetiradaProdutoCommand request)
            {
                try
                {
                    var command = new RetiraQuantidadeProdutoCommand { Id = request.IdProduto, Quantidade = request.Quantidade };
                    await _mediator.Send(command);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
            }
        }
    }
}