namespace Controle.Estoque.Application.DTOs
{
    public class RelatorioDTO
    {
        public string DescricaoProduto { get; set; }
        public string PartNumber { get; set; }
        public int QuantidadePecasRetiradas { get; set; }
        public decimal ValorCusto { get; set; }
        public DateTime DataRetiradaProduto { get; set; }
    }
}