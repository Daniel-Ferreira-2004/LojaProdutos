using LojaProdutos.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Services.Produtos
{
    public interface IProdutosInterface 
    {
        Task<List<ProdutosModel>> BuscarProdutos();
    }
}
