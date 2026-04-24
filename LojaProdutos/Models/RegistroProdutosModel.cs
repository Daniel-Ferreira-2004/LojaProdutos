namespace LojaProdutos.Models
{
    public class RegistroProdutosModel
    {
        public int ProdutoId { get; set; }
        public string CategoriaNome { get; set; }
        public decimal Total { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal TotalGeral { get; set; }
    }
}
