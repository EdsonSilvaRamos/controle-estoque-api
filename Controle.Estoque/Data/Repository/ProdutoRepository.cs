using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Controle.Estoque.Data.Repository
{
    public class ProdutoRepository : IRepository<Produto>
    {
        private readonly EstoqueContext _context;

        public ProdutoRepository(EstoqueContext context)
        {
            _context = context;
        }

        public async Task<Produto> GetById(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                return null;

            return produto;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            var listaProduto = await _context.Produtos.ToListAsync();
            return listaProduto ?? Enumerable.Empty<Produto>();
        }

        public async Task Add(Produto item)
        {
            await _context.AddAsync(item);
        }

        public void Delete(Produto item)
        {
            _context.Remove(item);
        }

        public void Edit(Produto item)
        {
            _context.Update(item);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}