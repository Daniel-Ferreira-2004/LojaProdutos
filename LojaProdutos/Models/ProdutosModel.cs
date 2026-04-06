using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LojaProdutos.Models
{
    public class ProdutosModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nome não preenchido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Marca não preenchido")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Valor não preenchido")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Quantidade não preenchido")]
        public int QuantidadeEstoque { get; set; }

        [Required(ErrorMessage = "Descrição não preenchido")]
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public int CategoriaModelId { get; set; }

        [ValidateNever]
        public CategoriaModel Categoria { get; set; }
    }
}
