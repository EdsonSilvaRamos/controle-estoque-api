using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Repository;
using MediatR;

namespace Controle.Estoque.Application.Commands.CommandLogErro
{
    public class CriacaoLogErroCommand : IRequest<LogErro>
    {
        public string DescricaoErro { get; set; }

        public class CriacaoLogErroCommandHandler : IRequestHandler<CriacaoLogErroCommand, LogErro>
        {
            private readonly IMediator _mediator;
            private readonly IRepository<LogErro> _repository;

            public CriacaoLogErroCommandHandler(IMediator mediator, IRepository<LogErro> repository)
            {
                _mediator = mediator;
                _repository = repository;
            }

            public async Task<LogErro> Handle(CriacaoLogErroCommand request, CancellationToken cancellationToken)
            {
                var novoLogErro = new LogErro { DescricaoErro = request.DescricaoErro };

                await _repository.Add(novoLogErro);
                await _repository.SaveChanges();

                return novoLogErro;
            }
        }
    }
}