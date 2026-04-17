using System.Diagnostics;
using LojaProdutos.Models;
using LojaProdutos.Services.Produtos;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutosInterface _produtosService;

        public HomeController(IProdutosInterface produtosInterface)
        {
            _produtosService = produtosInterface;
        }

        public async Task<IActionResult> Index(string? pesquisar)
        {
            List<ProdutosModel> produtos = new List<ProdutosModel>();
            if (pesquisar == null)
            {
                produtos = await _produtosService.BuscarProdutos();
            }
            else
            {
                produtos = await _produtosService.BuscarProdutosFiltro(pesquisar);
            }

            return View(produtos);
        }
    }
}
