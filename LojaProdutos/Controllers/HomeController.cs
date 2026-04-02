using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
