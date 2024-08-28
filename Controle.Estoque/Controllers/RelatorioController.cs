using Controle.Estoque.Application.Queries.QueriesRelatorio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controle.Estoque.Controllers
{
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly IMediator _mediator;       

        public RelatorioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> RetornaRelatorio()
        {
            var command = new RetornaRelatorioQuantidadeRetiradasPorDataQuery();
            var relatorio = await _mediator.Send(command);
            return Ok(relatorio);
        }
    }
}