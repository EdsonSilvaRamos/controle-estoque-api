using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Controle.Estoque.Data.Repository
{
    public class RetiradaProdutoRepository : IRepository<RetiradaProduto>
    {
        private readonly EstoqueContext _context;

        public RetiradaProdutoRepository(EstoqueContext context)
        {
            _context = context;
        }

        public async Task<RetiradaProduto> GetById(Guid id)
        {
            var pedidoProduto = await _context.RetiradasProduto.FindAsync(id);

            if (pedidoProduto == null)
                //throw new ApplicationException("Pedido Produto não encontrado");
                return null;

            return pedidoProduto;
        }

        public async Task<IEnumerable<RetiradaProduto>> GetAll()
        {
            var listaRetiradaProduto = await _context.RetiradasProduto.ToListAsync();
            return listaRetiradaProduto ?? Enumerable.Empty<RetiradaProduto>();
        }

        public async Task Add(RetiradaProduto item)
        {
            await _context.AddAsync(item);
        }

        public void Edit(RetiradaProduto item)
        {
            _context.RetiradasProduto.Update(item);
        }

        public void Delete(RetiradaProduto item)
        {            
            _context.RetiradasProduto.Remove(item);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}