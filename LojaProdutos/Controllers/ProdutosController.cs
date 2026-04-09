using LojaProdutos.Models;
using LojaProdutos.Services.Categorias;
using LojaProdutos.Services.Produtos;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutosInterface  _produtosInterface;
        private readonly ICategoriaInterface _categoriasInterface;

        public ProdutosController(IProdutosInterface produtosInterface, ICategoriaInterface categoriasInterface)
        {
            _produtosInterface = produtosInterface;
            _categoriasInterface = categoriasInterface;
        }
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtosInterface.BuscarProdutos();
            return View(produtos);
        }

        public async Task<IActionResult> Cadastrar()
        {
            var categorias = await _categoriasInterface.BuscarCategorias();
            ViewBag.Categorias = categorias;
            return View();
        }
    }
}
