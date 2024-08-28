using Controle.Estoque.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Controle.Estoque.Data.Context
{
    public class EstoqueContext : DbContext
    {
        public EstoqueContext(DbContextOptions<EstoqueContext> options) : base(options) { }

        public DbSet<RetiradaProduto> RetiradasProduto { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<LogErro> LogsErro { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<LogErro>().ToTable("LogsErro");
            modelBuilder.Entity<RetiradaProduto>().ToTable("RetiradasProduto");
        }
    }
}