using LojaProdutos.DTO.Produto;
using LojaProdutos.DTO.Produtos;
using LojaProdutos.Models;
using LojaProdutos.Services.Categorias;
using LojaProdutos.Services.Produtos;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutosInterface _produtosInterface;
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

        public async Task<IActionResult> Editar(int id)
        {
            var produtos = await _produtosInterface.GetProdutosId(id);

            var editarProdutoDTO = new EditarProdutoDTO
            {
                Nome = produtos.Nome,
                Marca = produtos.Marca,
                Valor = produtos.Valor,
                QuantidadeEstoque = produtos.QuantidadeEstoque,
                Foto = produtos.Foto,
                CategoriaModelId = produtos.CategoriaModelId,

            };
            ViewBag.Categorias = await _categoriasInterface.BuscarCategorias();

            return View(editarProdutoDTO);

        }

        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var produto = await _produtosInterface.Remover(id);
                return RedirectToAction("Index", "Produtos");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CriarProdutoDTO criarProdutoDTO, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                var produto = await _produtosInterface.Cadastrar(criarProdutoDTO, foto);
                TempData["Sucesso"] = "Produto cadastrado com sucesso!";
                return RedirectToAction("Index", "Produtos");
            }
            else
            {
                ViewBag.Categorias = await _categoriasInterface.BuscarCategorias();
                TempData["Erro"] = "Erro ao cadastrar o produto. Verifique os dados e tente novamente.";
                return View(criarProdutoDTO);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarProdutoDTO editarProdutoDTO, IFormFile foto)
        {
            if (!ModelState.IsValid)
            {
                var produto = await _produtosInterface.Editar(editarProdutoDTO, foto);
                TempData["Sucesso"] = "Produto editado com sucesso!";
                return RedirectToAction("Index", "Produtos");
            }
            else
            {
                ViewBag.Categorias = await _categoriasInterface.BuscarCategorias();
                TempData["Erro"] = "Erro ao editar o produto. Verifique os dados e tente novamente.";
                return View(editarProdutoDTO);

            }
        }
    }
}
