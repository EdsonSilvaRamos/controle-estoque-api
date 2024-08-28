using Controle.Estoque.Application.Commands.CommandPedidoProduto;
using Controle.Estoque.Application.Queries.QueriesRetiradaProduto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controle.Estoque.Controllers
{
    [Route("api/[controller]")]
    public class RetiradaProdutoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RetiradaProdutoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new RetornaListaRetiradaProdutoQuery();
            var listaRetiradaProduto = await _mediator.Send(command);
            return Ok(listaRetiradaProduto);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new RetornaRetiradaProdutoPorIdQuery { Id = id };
            var retiradaProduto = await _mediator.Send(command);
            if (retiradaProduto == null)
                return NotFound("Retirada de produto não encontrada");

            return Ok(retiradaProduto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriacaoRetiradaProdutoCommand command)
        {
            try
            {
                var retiradaProdutoCriado = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = retiradaProdutoCriado.Id }, retiradaProdutoCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}