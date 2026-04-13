using System.ComponentModel.DataAnnotations;

namespace LojaProdutos.DTO.Produto
{
    public class CriarProdutoDTO
    {
        [Required(ErrorMessage = "Nome não preenchido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Marca não preenchido")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Valor não preenchido")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Quantidade não preenchido")]
        public int QuantidadeEstoque { get; set; }
        public string? Foto { get; set; }
        public int CategoriaModelId { get; set; }
    }
}
