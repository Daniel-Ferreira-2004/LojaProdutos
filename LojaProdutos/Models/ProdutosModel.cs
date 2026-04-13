using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LojaProdutos.Models
{
    public class ProdutosModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string? Descricao { get; set; }
        public string Foto { get; set; }
        public int CategoriaModelId { get; set; }

        [ValidateNever]
        public CategoriaModel Categoria { get; set; }
    }
}
