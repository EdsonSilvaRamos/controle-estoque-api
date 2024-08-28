namespace Controle.Estoque.Application.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string PartNumber { get; set; }
        public decimal ValorCusto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }


        public void Atualiza(string descricao, string partNumber, decimal valorCusto, DateTime dataAtualizacao)
        {
            Descricao = !string.IsNullOrEmpty(descricao) ? descricao : Descricao;
            PartNumber = !string.IsNullOrEmpty(partNumber) ? partNumber : PartNumber;
            ValorCusto = valorCusto > 0 ? valorCusto : ValorCusto;
            DataAtualizacao = dataAtualizacao;
        }

        public void AdicionaQuantidade(int quantidade, DateTime dataAtualizacao)
        {
            Quantidade += quantidade;
            DataAtualizacao = dataAtualizacao;
        }

        public void RetiraQuantidade(int quantidade, DateTime dataAtualizacao)
        {
            Quantidade -= quantidade;
            DataAtualizacao = dataAtualizacao;
        }
    }
}