using Controle.Estoque.Application.Commands.CommandProduto;
using Controle.Estoque.Application.Queries.QueriesProduto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controle.Estoque.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdutoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new RetornaListaProdutoQuery();
            var listaProduto = await _mediator.Send(command);
            return Ok(listaProduto);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new RetornaProdutoPorIdQuery{ Id = id };
            var produto = await _mediator.Send(command);
            if (produto == null)
                return NotFound("Produto não encontrado");

            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriacaoProdutoCommand command)
        {
            var produtoCriado = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = produtoCriado.Id }, produtoCriado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AtualizaProdutoCommand command)
        {
            command.Id = id;
            var produtoAtualizado = await _mediator.Send(command);

            return produtoAtualizado != null ? Ok(produtoAtualizado) : NotFound("Produto não encontrado");
        }

        [HttpPut("adicionaQuantidade/{id}")]
        public async Task<IActionResult> AdicionaQuantidadeProduto(Guid id, [FromBody] AdicionaQuantidadeProdutoCommand command)
        {
            command.Id = id;
            var produtoAtualizado = await _mediator.Send(command);

            return produtoAtualizado != null ? Ok(produtoAtualizado) : NotFound("Produto não encontrado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(Guid id)
        {
            var command = new DeletaProdutoCommand { Id = id };
            var produtoDeletado = await _mediator.Send(command);

            return produtoDeletado != null ? Ok(produtoDeletado) : NotFound("Produto não encontrado");
        }
    }
}