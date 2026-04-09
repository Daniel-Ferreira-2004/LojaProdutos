using LojaProdutos.Models;

namespace LojaProdutos.Services.Categorias
{
    public interface ICategoriaInterface
    {
            Task<List<CategoriaModel>> BuscarCategorias();
    }
}
