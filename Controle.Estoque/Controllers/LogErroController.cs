using Controle.Estoque.Application.Queries.QueriesLogErro;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controle.Estoque.Controllers
{
    [Route("api/[controller]")]
    public class LogErroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogErroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new RetornaListaLogErroQuery();
            var listaLogErro = await _mediator.Send(command);
            return Ok(listaLogErro);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new RetornaLogErroPorIdQuery { Id = id };
            var logErro = await _mediator.Send(command);
            if (logErro == null)
                return NotFound("Log de erro não encontrado");

            return Ok(logErro);
        }
    }
}