using LojaProdutos.DTO.Produto;
using LojaProdutos.DTO.Produtos;
using LojaProdutos.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Services.Produtos
{
    public interface IProdutosInterface 
    {
        Task<List<ProdutosModel>> BuscarProdutos();
        Task<List<ProdutosModel>> BuscarProdutosFiltro(string pesquisar);
        Task<ProdutosModel> Cadastrar(CriarProdutoDTO criarProdutoDTO, IFormFile foto);
        Task<ProdutosModel> GetProdutosId(int id);
        Task<ProdutosModel> Editar(EditarProdutoDTO editarProdutoDTO, IFormFile? foto);
        Task<ProdutosModel> Remover(int id);
    }
}
