using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LojaProdutos.Models
{
    public class EstoqueModel
    {
        public int Id { get; set; }
        public int ProdutoModelId { get; set; }
        [ValidateNever]
        public ProdutosModel ProdutosModel { get; set; }
        public DateTime DataBaixa { get; set; } = DateTime.Now;
    }
}
