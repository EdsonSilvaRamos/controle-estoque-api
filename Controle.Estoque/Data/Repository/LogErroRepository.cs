using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Controle.Estoque.Data.Repository
{
    public class LogErroRepository : IRepository<LogErro>
    {
        private readonly EstoqueContext _context;

        public LogErroRepository(EstoqueContext context)
        {
            _context = context;
        }

        public async Task<LogErro> GetById(Guid id)
        {
            var logErro = await _context.LogsErro.FindAsync(id);

            if (logErro == null)
                return null;

            return logErro;
        }

        public async Task<IEnumerable<LogErro>> GetAll()
        {
            var listaErros = await _context.LogsErro.ToListAsync();
            return listaErros ?? Enumerable.Empty<LogErro>();
        }

        public async Task Add(LogErro item)
        {
            await _context.AddAsync(item);
        }

        public void Edit(LogErro item)
        {
            _context.Update(item);
        }

        public void Delete(LogErro item)
        {
            _context.Remove(item);
        }
        
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}