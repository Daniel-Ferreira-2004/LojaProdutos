using System.ComponentModel.DataAnnotations;

namespace LojaProdutos.Models
{
    public class CategoriaModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Nome da categoria não preenchido.") ]
        public string Nome { get; set; }
    }
}
