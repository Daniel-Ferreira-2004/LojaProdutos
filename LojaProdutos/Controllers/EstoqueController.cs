using LojaProdutos.Services.Estoque;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LojaProdutos.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly IEstoqueInterface _estoqueInterface;
        public EstoqueController(IEstoqueInterface estoqueInterface)
        {
            _estoqueInterface = estoqueInterface;
        }

        public IActionResult Index()
        {
            var registros = _estoqueInterface.ListagemRegistro();
            return View(registros);
        }
        [HttpPost]
        public async Task<IActionResult> BaixarEstoque(int id)
        {
            var produtoBaixado = await _estoqueInterface.CriarRegistro(id);
            TempData["MensagemSucesso"] = "Produto baixado com sucesso!";
            return RedirectToAction("Index", "Home");
        }

    }
}
