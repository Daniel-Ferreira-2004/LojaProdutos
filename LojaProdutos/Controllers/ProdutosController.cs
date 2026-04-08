using LojaProdutos.Models;
using LojaProdutos.Services.Produtos;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutosInterface  _produtosInterface;

        public ProdutosController(IProdutosInterface produtosInterface)
        {
            _produtosInterface = produtosInterface;
        }
        public async Task<IActionResult> BuscarProdutos()
        {
            var produtos = await _produtosInterface.BuscarProdutos();
            return View(produtos);
        }
    }
}
