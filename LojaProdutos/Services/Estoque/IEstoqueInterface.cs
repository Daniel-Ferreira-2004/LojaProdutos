using LojaProdutos.Models;

namespace LojaProdutos.Services.Estoque
{
    public interface IEstoqueInterface
    {
        public Task<EstoqueModel> CriarRegistro(int idProduto);
    }
}
