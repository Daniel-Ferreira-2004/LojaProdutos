using LojaProdutos.Models;

namespace LojaProdutos.Services.Estoque
{
    public interface IEstoqueInterface
    {
        Task<EstoqueModel> CriarRegistro(int idProduto);
        List<RegistroProdutosModel> ListagemRegistro();

    }
}
