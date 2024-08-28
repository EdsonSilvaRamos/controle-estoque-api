namespace Controle.Estoque.Application.Models
{
    public class RetiradaProduto
    {
        public Guid Id { get; set; }
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorCusto { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}