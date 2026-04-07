using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Controllers
{
    public class ProdutosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
